
GameUI = {}

local UnityGameObject = UnityEngine.GameObject
local GameUIManager = UnityGameObject.Find("UIManager"):GetComponent("GameUIManager")

function GameUI.getHit()
    GameUIManager:UpdateAllHpBar()
end