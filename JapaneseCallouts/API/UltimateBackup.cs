using UltimateBackup.API;
using UBFunctions = UltimateBackup.API.Functions;

namespace JapaneseCallouts.API;

internal static class UltimateBackupFunctions
{
    /// <summary>
    /// Code2の応援を要請
    /// </summary>
    internal static void CallCode2Backup()
    => UBFunctions.callCode2Backup();

    /// <summary>
    /// Code2の応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    internal static void CallCode2Backup(bool isPlayingRadioAnimation)
    => UBFunctions.callCode2Backup(isPlayingRadioAnimation);

    /// <summary>
    /// Code2の応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallCode2Backup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callCode2Backup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// Code3の応援を要請
    /// </summary>
    internal static void CallCode3Backup()
    => UBFunctions.callCode3Backup();

    /// <summary>
    /// Code3の応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    internal static void CallCode3Backup(bool isPlayingRadioAnimation)
    => UBFunctions.callCode3Backup(isPlayingRadioAnimation);

    /// <summary>
    /// Code3の応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallCode3Backup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callCode3Backup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// Code3でSWATを要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isNooseUnit">NOOSEにするか</param>
    internal static void CallCode3SwatBackup(bool isPlayingRadioAnimation, bool isNooseUnit)
    => UBFunctions.callCode3SwatBackup(isPlayingRadioAnimation, isNooseUnit);

    /// <summary>
    /// Felony式職質舞台を要請
    /// </summary>
    internal static void CallFelonyStopBackup()
    => UBFunctions.callFelonyStopBackup();

    /// <summary>
    /// Felony式職質舞台を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallFelonyStopBackup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callFelonyStopBackup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// 女性警官を要請
    /// </summary>
    internal static void CallFemaleBackup()
    => UBFunctions.callFemaleBackup();

    /// <summary>
    /// 女性警官を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallFemaleBackup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callFemaleBackup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// 消防車を要請
    /// </summary>
    internal static void CallFireDepartment()
    => UBFunctions.callFireDepartment();

    /// <summary>
    /// 救急車を要請
    /// </summary>
    internal static void CallAmbulance()
    => UBFunctions.callAmbulance();

    /// <summary>
    /// 集団応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation"></param>
    internal static void CallGroupBackup(bool isPlayingRadioAnimation)
    => UBFunctions.callGroupBackup(isPlayingRadioAnimation);

    /// <summary>
    /// 警察犬を要請
    /// </summary>
    internal static void CallK9Backup()
    => UBFunctions.callK9Backup();

    /// <summary>
    /// 警察犬を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallK9Backup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callK9Backup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// 緊急発報
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    internal static void CallPanicButtonBackup(bool isPlayingRadioAnimation)
    => UBFunctions.callPanicButtonBackup(isPlayingRadioAnimation);

    /// <summary>
    /// 追跡応援を要請
    /// </summary>
    internal static void CallPursuitBackup()
    => UBFunctions.callPursuitBackup();

    /// <summary>
    /// 追跡応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    internal static void CallPursuitBackup(bool isPlayingRadioAnimation)
    => UBFunctions.callPursuitBackup(isPlayingRadioAnimation);

    /// <summary>
    /// 追跡応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallPursuitBackup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callPursuitBackup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// 検問設置を要請
    /// </summary>
    internal static void CallRoadBlockBackup()
    => UBFunctions.callRoadBlockBackup();

    /// <summary>
    /// スパイク班を要請
    /// </summary>
    internal static void CallSpikeStripsBackup()
    => UBFunctions.callSpikeStripsBackup();

    /// <summary>
    /// 職質応援を要請
    /// </summary>
    internal static void CallTrafficStopBackup()
    => UBFunctions.callTrafficStopBackup();

    /// <summary>
    /// 職質応援を要請
    /// </summary>
    /// <param name="isPlayingRadioAnimation">無線アニメーションの有無</param>
    /// <param name="isStatePatrol">StatePatrolにするか</param>
    internal static void CallTrafficStopBackup(bool isPlayingRadioAnimation, bool isStatePatrol)
    => UBFunctions.callTrafficStopBackup(isPlayingRadioAnimation, isStatePatrol);

    /// <summary>
    /// 全ての応援を解散
    /// </summary>
    internal static void DismissAllBackupUnits()
    => UBFunctions.dismissAllBackupUnits();

    /// <summary>
    /// LocalPatrolの警官を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="heading">向き</param>
    /// <returns>LocalPatrolの警官</returns>
    internal static Ped GetLocalPatrolPed(Vector3 location, float heading)
    => UBFunctions.getLocalPatrolPed(location, heading);

    /// <summary>
    /// StatePatrolの警官を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="heading">向き</param>
    /// <returns>StatePatrolの警官</returns>
    internal static Ped GetStatePatrolPed(Vector3 location, float heading)
    => UBFunctions.geStatePatrolPed(location, heading);

    /// <summary>
    /// 救急隊員を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="heading">向き</param>
    /// <returns>救急隊員</returns>
    internal static Ped GetAmbulancePed(Vector3 location, float heading)
    => UBFunctions.getAmbulancePed(location, heading);

    /// <summary>
    /// 救急車と救急隊員を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>救急車と救急隊員</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetAmbulanceUnit(Vector3 location, int numPeds)
    => UBFunctions.getAmbulanceUnit(location, numPeds);

    /// <summary>
    /// 検視官と車両を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>検視官と車両</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetCoronerUnit(Vector3 location, int numPeds)
    => UBFunctions.getCoronerUnit(location, numPeds);

    /// <summary>
    /// 消防車と消防隊員を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>消防車と消防隊員</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetFireTruckUnit(Vector3 location, int numPeds)
    => UBFunctions.getFireTruckUnit(location, numPeds);

    /// <summary>
    /// LocalPatrol部隊を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>LocalPatrol部隊</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetLocalPatrolUnit(Vector3 location, int numPeds)
    => UBFunctions.getLocalPatrolUnit(location, numPeds);

    /// <summary>
    /// StatePatrol部隊を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>StatePatrol部隊</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetStatePatrolUnit(Vector3 location, int numPeds)
    => UBFunctions.getStatePatrolUnit(location, numPeds);

    /// <summary>
    /// LocalSWAT部隊を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>LocalSWAT部隊</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetLocalSWATUnit(Vector3 location, int numPeds)
    => UBFunctions.getLocalSWATUnit(location, numPeds);

    /// <summary>
    /// NooseSWAT部隊を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>NooseSWAT部隊</returns>
    internal static Tuple<Vehicle, List<Ped>>  GetNooseSWATUnit(Vector3 location, int numPeds)
    => UBFunctions.getNooseSWATUnit(location, numPeds);

    /// <summary>
    /// 移送部隊を取得
    /// </summary>
    /// <param name="location">位置</param>
    /// <param name="numPeds">人数</param>
    /// <returns>移送部隊</returns>
    internal static Tuple<Vehicle, List<Ped>> GetPoliceTransportUnit(Vector3 location, int numPeds)
    => UBFunctions.getPoliceTransportUnit(location, numPeds);

    /// <summary>
    /// UltimateBackupの管理下にあるPedかどうか
    /// </summary>
    /// <param name="ped">検査対象</param>
    /// <returns>真偽</returns>
    internal static bool IsUltimateBackupCop(Ped ped)
    => UBFunctions.isUltimateBackupCop(ped);
}