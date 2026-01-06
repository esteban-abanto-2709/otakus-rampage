// Todos los Enums usados en el juego

public enum eEntityState {
    Idle,
    Walk,
    Attack,
    Stunt,
    Death
}

public enum eTypeEnemy
{
    Fase_I,
    Boss
}

public enum eModeGun {
    manual,
    auto
}

public enum eTypeConsume {
    normal,
    unlimied
}

public enum eLevelTower {
    nivel_0,
    nivel_1,
    nivel_2
}

public enum eLevelFase {
    nivel_0,
    nivel_1,
    nivel_2
}

public enum ePanelState {
    withoutPanel,
    pausePanel,
    optionPanel,
    controlPanel,
    weaponShopPanel,
    towerShopPanel
}

public enum eStateTowerShop {
    regenerator,
    waifu,
    machineGun,
    Bubble,
    noTower
}

public enum eTargetEnemy {
    rocket,
    player,
    waifu
}