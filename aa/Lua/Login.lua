
Login = {}

local UnityGameObject = UnityEngine.GameObject
local LoginManager = UnityGameObject.Find("LoginManager"):GetComponent("LoginManager")
local Color = UnityEngine.Color

function Login.log(user,psd)
    LoginManager.reminderPanel : SetActive(true)

    if user==psd then
        LoginManager.sucWd : SetActive(true)
    else
        LoginManager.failWd : SetActive(true)
    end
end

function Login.returnLog()
    print("back to login")
    LoginManager.reminderPanel : SetActive(false)
    LoginManager.sucWd : SetActive(false)
    LoginManager.failWd : SetActive(false)
end
