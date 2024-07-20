namespace JapaneseCallouts.Callouts.PacificBankHeist;

/// <summary>
/// 現在のコールアウトの状況
/// </summary>
internal enum EPacificBankHeistStatus : uint
{
    /// <summary>
    /// 指揮官と話す前
    /// 必ず通過する
    /// </summary>
    Init = 1u,

    /// <summary>
    /// 指揮官と対話中
    /// 必ず通過する
    /// </summary>
    TalkingToCommander = 2u,

    /// <summary>
    /// 強盗と交渉中
    /// 交渉を選んだときのみ通過
    /// </summary>
    NegotiatingToRobbers = 4u,

    /// <summary>
    /// 戦闘の準備中
    /// 交渉に失敗するか、戦闘を選んだときのみ通過
    /// </summary>
    FightingPreparing = 8u,

    /// <summary>
    /// 強盗と戦闘中
    /// 交渉に失敗するか、戦闘を選んだときのみ通過
    /// </summary>
    FightingWithRobbers = 16u,

    /// <summary>
    /// 降伏中
    /// 交渉に成功したときのみ通過
    /// </summary>
    Surrendering = 32u,

    /// <summary>
    /// 指揮官と2回目の対話が可能である状態
    /// 強盗全員が降伏したときのみ通過
    /// </summary>
    BeforeTalkingToCommander2nd = 64u,

    /// <summary>
    /// 指揮官と2回目の対話中
    /// 強盗全員が降伏したときのみ通過
    /// </summary>
    TalkingToCommander2nd = 128u,

    /// <summary>
    /// 最終処理
    /// 必ず通過する
    /// </summary>
    Last = 256u,
}

/// <summary>
/// アラームの状態を管理する
/// </summary>
internal enum AlarmState : int
{
    /// <summary>
    /// 何もなし
    /// </summary>
    None = -1,
    /// <summary>
    /// アラームが有効
    /// </summary>
    Alarm = 0,
}