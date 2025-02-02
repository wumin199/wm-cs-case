#
# Administration permission required
# open powershell
# Set-ExecutionPolicy Bypass -Scope Process -Force
# ./openssh.ps1
#
$opensshInstalled = Get-WindowsCapability -Online | Where-Object { $_.Name -like 'OpenSSH.Server*' }

if ($opensshInstalled.State -eq "NotPresent"){
    Write-Output "Openssh is not installed"
    Write-Output "Installing OpenSSH"
    Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0

    # check if OpenSSH is installed
    $sshInstalled = Get-WindowsCapability -Online | Where-Object { $_.Name -like "OpenSSH.Server*" -and $_.State -eq "Installed" }
    if ($sshInstalled) {
        Write-Host "OpenSSH is already installed"
    } else {
        Write-Host "OpenSSH installed failed"
        exit
    }
}

$service = Get-Service -Name sshd -ErrorAction SilentlyContinue

if ($service -eq $null) {
    Write-Output "SSH not installed"
    exit
} else {
    if ($service.Status -ne 'Running') {
        Write-Output "Starting SSH service"
        Start-Service -Name sshd
        Set-Service -Name sshd -StartupType Automatic
        Write-Output "SSH service started."
    } else {
        Write-Output "SSH service is already running"
    }
}