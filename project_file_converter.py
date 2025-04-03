import os
import subprocess
import time
import win32clipboard
import win32con
import win32gui
import win32process
import keyboard
from pathlib import Path

def get_clipboard_text():
    win32clipboard.OpenClipboard()
    try:
        text = win32clipboard.GetClipboardData(win32con.CF_UNICODETEXT)
    except:
        text = ""
    finally:
        win32clipboard.CloseClipboard()
    return text

def wait_for_notepad(process, timeout=5):
    """等待记事本窗口出现并准备就绪"""
    start_time = time.time()
    while time.time() - start_time < timeout:
        # 查找所有记事本窗口
        def callback(hwnd, hwnds):
            if win32gui.IsWindowVisible(hwnd):
                _, pid = win32process.GetWindowThreadProcessId(hwnd)
                if pid == process.pid:
                    hwnds.append(hwnd)
            return True
        
        hwnds = []
        win32gui.EnumWindows(callback, hwnds)
        
        if hwnds:
            # 等待窗口完全加载
            time.sleep(0.1)
            return True
        time.sleep(0.1)
    return False

def is_file_encrypted(file_path):
    """检查文件是否被加密"""
    try:
        # 尝试以UTF-8编码读取文件
        with open(file_path, 'r', encoding='utf-8') as f:
            # 读取所有行，但最多读取10行
            first_lines = []
            for i, line in enumerate(f):
                if i >= 10:  # 最多读取10行
                    break
                first_lines.append(line)
            
            if not first_lines:  # 如果文件为空
                return True
                
            content = ''.join(first_lines)
            # 检查是否包含常见的C#关键字和符号
            cs_keywords = ['using', 'namespace', 'public', 'private', 'class', '{', '}']
            has_cs_features = any(keyword in content for keyword in cs_keywords)
            return not has_cs_features  # 如果没有C#特征，可能是加密文件
    except UnicodeDecodeError:
        # 如果UTF-8解码失败，说明可能是加密文件
        return True
    except Exception:
        # 其他错误也视为需要处理
        return True

def convert_file(source_file):
    """转换单个文件的内容"""
    try:
        # 首先检查文件是否需要转换
        if not is_file_encrypted(source_file):
            print(f"\n文件未加密，跳过处理: {source_file}")
            return True, None
            
        # 创建临时文件名
        temp_file = str(source_file) + '.temp'
        source_file = str(source_file)
        
        print(f"\n处理加密文件: {source_file}")
        
        # 打开记事本并等待窗口出现
        print("正在打开记事本...")
        notepad = subprocess.Popen(['notepad.exe', source_file])
        if not wait_for_notepad(notepad):
            error_msg = "等待记事本窗口超时"
            print(error_msg)
            return False, error_msg
        
        # 模拟按键 Ctrl+A (全选) 和 Ctrl+C (复制)
        print("正在复制文件内容...")
        keyboard.press_and_release('ctrl+a')
        time.sleep(0.1)  # 短暂等待选中完成
        keyboard.press_and_release('ctrl+c')
        time.sleep(0.1)  # 短暂等待复制完成
        
        # 关闭记事本
        print("正在关闭记事本...")
        keyboard.press_and_release('alt+f4')
        time.sleep(0.1)  # 短暂等待关闭对话框出现
        keyboard.press_and_release('n')  # 按"不保存"
        
        # 获取剪贴板内容
        content = get_clipboard_text()
        
        if content:
            # 处理内容，统一换行符并移除多余的空行
            lines = content.splitlines()  # 这会自动处理所有类型的换行符
            content = '\n'.join(lines)  # 使用单个换行符重新组合
            
            # 先将内容写入临时文件
            with open(temp_file, 'w', encoding='utf-8', newline='\n') as f:
                f.write(content)
            
            # 检查临时文件是否写入成功且内容正确
            try:
                with open(temp_file, 'r', encoding='utf-8') as f:
                    check_content = f.read()
                if check_content == content:
                    # 内容验证成功，删除原文件，将临时文件重命名为原文件名
                    os.remove(source_file)
                    os.rename(temp_file, source_file)
                    print(f"成功！文件已转换并替换原文件")
                    return True, None
                else:
                    error_msg = "文件内容验证失败"
                    print(error_msg)
                    os.remove(temp_file)  # 清理临时文件
                    return False, error_msg
            except Exception as e:
                error_msg = f"文件验证时发生错误: {str(e)}"
                print(error_msg)
                if os.path.exists(temp_file):
                    os.remove(temp_file)  # 清理临时文件
                return False, error_msg
        else:
            error_msg = "无法从剪贴板获取内容"
            print(error_msg)
            return False, error_msg
            
    except Exception as e:
        error_msg = f"处理文件时发生错误: {str(e)}"
        print(error_msg)
        # 清理临时文件
        if os.path.exists(temp_file):
            os.remove(temp_file)
        return False, error_msg

def get_case_combinations(word):
    """生成一个单词的所有大小写组合"""
    if not word:
        return ['']
    first, rest = word[0], word[1:]
    combinations = get_case_combinations(rest)
    return [first.lower() + c for c in combinations] + [first.upper() + c for c in combinations]

# 生成所有可能的大小写组合
exclude_patterns = ['bin', 'obj']
exclude_dirs = set()
for pattern in exclude_patterns:
    exclude_dirs.update(get_case_combinations(pattern))

def process_project():
    """处理整个项目中的.cs文件"""
    # 获取脚本所在目录的绝对路径
    script_dir = os.path.dirname(os.path.abspath(__file__))
    # 获取项目根目录（脚本所在目录）
    project_root = script_dir
    
    # 统计信息
    total_files = 0
    success_files = 0
    skipped_files = 0  # 跳过的未加密文件
    failed_files = []  # 元组列表，每个元组包含 (文件路径, 错误原因)
    success_files_list = []  # 成功转换的文件列表
    
    print(f"开始处理项目: {project_root}")
    print("正在搜索.cs文件...")
    
    # 遍历所有.cs文件
    for root, dirs, files in os.walk(project_root):
        # 排除bin和obj目录（不区分大小写）
        dirs[:] = [d for d in dirs if d not in exclude_dirs]
        
        for file in files:
            if file.endswith('.cs'):
                total_files += 1
                file_path = Path(os.path.join(root, file))
                
                # 将文件路径转换为相对于项目根目录的路径（用于显示）
                try:
                    rel_path = os.path.relpath(file_path, project_root)
                except ValueError:
                    # 如果在不同驱动器上，使用绝对路径
                    rel_path = str(file_path)
                
                try:
                    if not is_file_encrypted(file_path):
                        print(f"\n文件未加密，跳过处理: {rel_path}")
                        skipped_files += 1
                        continue
                        
                    success, error_msg = convert_file(file_path)
                    if success:
                        success_files += 1
                        success_files_list.append(rel_path)
                    else:
                        failed_files.append((rel_path, error_msg))
                except Exception as e:
                    error_msg = f"处理文件时发生异常: {str(e)}"
                    print(error_msg)
                    failed_files.append((rel_path, error_msg))
    
    # 打印处理结果
    print("\n" + "="*50)
    print("处理完成！")
    print("="*50)
    print(f"项目根目录: {project_root}")
    print(f"总共发现: {total_files} 个.cs文件")
    print(f"跳过处理: {skipped_files} 个未加密文件")
    print(f"成功转换: {success_files} 个加密文件")
    print(f"转换失败: {len(failed_files)} 个文件")
    
    # 打印成功转换的文件列表
    if success_files_list:
        print("\n成功转换的文件列表:")
        print("-"*50)
        for file_path in success_files_list:
            print(f"- {file_path}")
        print("-"*50)
    
    # 打印失败的文件列表
    if failed_files:
        print("\n转换失败的文件列表:")
        print("-"*50)
        for file_path, error_msg in failed_files:
            print(f"\n文件路径: {file_path}")
            print(f"失败原因: {error_msg}")
        print("-"*50)

if __name__ == "__main__":
    """
    使用方法：
    1. 安装必要的包：
       pip install pywin32 keyboard
    2. 将此脚本放在项目根目录下
    3. 运行脚本
    
    注意：
    - 脚本会自动排除 bin 和 obj 目录（不区分大小写）
    - 只处理加密的.cs文件，未加密的文件会跳过
    - 转换成功会直接替换原文件
    - 转换失败则保持原文件不变
    """
    
    # 确认是否继续
    print("="*50)
    print("此脚本将处理当前目录下所有的.cs文件（除了bin和obj目录，不区分大小写）")
    print("只处理加密的文件，未加密的文件会自动跳过")
    print("转换成功的文件将直接替换原文件，失败的文件将保持不变")
    print("="*50)
    input("按回车键开始处理，或按Ctrl+C取消...")
    
    try:
        process_project()
    except KeyboardInterrupt:
        print("\n用户取消操作")
    except Exception as e:
        print(f"\n发生错误: {str(e)}")
    
    print("\n按回车键退出...")
    input() 