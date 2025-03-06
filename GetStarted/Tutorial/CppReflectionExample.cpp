#include <string>
#include <map>
#include <functional>
#include <iostream>

// 简单的反射系统实现
class ReflectionSystem {
private:
    // 存储属性信息的结构
    struct PropertyInfo {
        std::string name;
        std::string description;
        std::function<void(void*)> getter;
        std::function<void(void*, const std::string&)> setter;
    };

    // 属性注册表
    std::map<std::string, PropertyInfo> properties;

public:
    // 注册属性
    template<typename T>
    void RegisterProperty(const std::string& name, 
                         const std::string& description,
                         T& (T::*getter)(),
                         void (T::*setter)(T)) {
        PropertyInfo info;
        info.name = name;
        info.description = description;
        
        // 包装getter
        info.getter = [getter](void* obj) {
            T* typed_obj = static_cast<T*>(obj);
            T& value = (typed_obj->*getter)();
            std::cout << value << std::endl;
        };

        // 包装setter
        info.setter = [setter](void* obj, const std::string& value) {
            T* typed_obj = static_cast<T*>(obj);
            T typed_value;
            // 这里需要类型转换，简化处理
            (typed_obj->*setter)(typed_value);
        };

        properties[name] = info;
    }

    // 获取属性信息
    void ShowProperty(const std::string& name, void* obj) {
        if (properties.find(name) != properties.end()) {
            auto& prop = properties[name];
            std::cout << "属性名: " << prop.name << std::endl;
            std::cout << "描述: " << prop.description << std::endl;
            std::cout << "值: ";
            prop.getter(obj);
        }
    }
};

// 示例类
class DatabaseConfig {
private:
    std::string server = "localhost";
    int port = 3306;

public:
    std::string& GetServer() { return server; }
    void SetServer(std::string value) { server = value; }
    
    int& GetPort() { return port; }
    void SetPort(int value) { port = value; }
};

// 使用示例
int main() {
    ReflectionSystem reflector;
    
    // 注册属性（这部分在实际应用中通常由代码生成器生成）
    DatabaseConfig config;
    reflector.RegisterProperty<std::string>(
        "Server",
        "数据库服务器地址",
        &DatabaseConfig::GetServer,
        &DatabaseConfig::SetServer
    );

    // 使用反射系统
    reflector.ShowProperty("Server", &config);
    
    return 0;
} 