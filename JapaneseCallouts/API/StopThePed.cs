using StopThePed.API;
using STPFunctions = StopThePed.API.Functions;

namespace JapaneseCallouts.API;

internal static class StopThePedFunctions
{
    /// <summary>
    /// AnimalControlを要請
    /// </summary>
    internal static void CallAnimalControl()
    => STPFunctions.callAnimalControl();

    /// <summary>
    /// 検視官を要請
    /// </summary>
    internal static void CallCoroner()
    => STPFunctions.callCoroner();

    /// <summary>
    /// 保険会社に連絡
    /// </summary>
    internal static void CallInsuranceService()
    => STPFunctions.callInsuranceService();

    /// <summary>
    /// 移送を要請
    /// </summary>
    internal static void CallPoliceTransport()
    => STPFunctions.callPoliceTransport();

    /// <summary>
    /// レッカー車を要請
    /// </summary>
    internal static void CallTowService()
    => STPFunctions.callTowService();

    /// <summary>
    /// 車両の保険の状態を取得
    /// </summary>
    /// <param name="vehicle">対象車両</param>
    /// <returns>状態</returns>
    internal static STPVehicleStatus GetVehicleInsuranceStatus(Vehicle vehicle)
    => STPFunctions.getVehicleInsuranceStatus(vehicle);

    /// <summary>
    /// 車両の登録状態を取得
    /// </summary>
    /// <param name="vehicle">対象車両</param>
    /// <returns>状態</returns>
    internal static STPVehicleStatus GetVehicleRegistrationStatus(Vehicle vehicle)
    => STPFunctions.getVehicleRegistrationStatus(vehicle);

    /// <summary>
    /// Pedに所持品検査のアイテムを追加
    /// </summary>
    /// <param name="ped">対象者</param>
    internal static void InjectPedSearchItems(Ped ped)
    => STPFunctions.injectPedSearchItems(ped);

    /// <summary>
    /// Vehicleに車両捜索のアイテムを追加
    /// </summary>
    /// <param name="vehicle">対象車両</param>
    internal static void InjectVehicleSearchItems(Vehicle vehicle)
    => STPFunctions.injectVehicleSearchItems(vehicle);

    /// <summary>
    /// Pedがアルコールの基準値を超えているかどうか
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <returns>真偽</returns>
    internal static bool IsPedAlcoholOverLimit(Ped ped)
    => STPFunctions.isPedAlcoholOverLimit(ped);

    /// <summary>
    /// Pedが掴まれているかどうか
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <returns>真偽</returns>
    internal static bool IsPedGrabbed(Ped ped)
    => STPFunctions.isPedGrabbed(ped);

    /// <summary>
    /// Pedが職質中かどうか
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <returns>真偽</returns>
    internal static bool IsPedStopped(Ped ped)
    => STPFunctions.isPedStopped(ped);

    /// <summary>
    /// Pedが薬物中毒かどうか
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <returns>真偽</returns>
    internal static bool IsPedUnderDrugsInfluence(Ped ped)
    => STPFunctions.isPedUnderDrugsInfluence(ped);

    /// <summary>
    /// フラッシュライトを使用しているかどうか
    /// </summary>
    /// <returns>真偽</returns>
    internal static bool IsPlayerUsingFlashlight()
    => STPFunctions.isPlayerUsingFlashlight();

    /// <summary>
    /// Pedの確認を要請
    /// </summary>
    /// <param name="isPlayingRadioAnim">無線アニメーションの有無</param>
    internal static void RequestDispatchPedCheck(bool isPlayingRadioAnim)
    => STPFunctions.requestDispatchPedCheck(isPlayingRadioAnim);

    /// <summary>
    /// Vehicleのナンバーの確認
    /// </summary>
    /// <param name="isPlayingRadioAnim">無線アニメーションの有無</param>
    internal static void RequestDispatchVehiclePlateCheck(bool isPlayingRadioAnim)
    => STPFunctions.requestDispatchVehiclePlateCheck(isPlayingRadioAnim);

    /// <summary>
    /// PITの許可の要請
    /// </summary>
    internal static void RequestPIT()
    => STPFunctions.requestPIT();

    /// <summary>
    /// Pedがアルコールの基準値を超えているか指定
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <param name="isOverLimit">超過しているかどうか</param>
    internal static void SetPedAlcoholOverLimit(Ped ped, bool isOverLimit)
    => STPFunctions.setPedAlcoholOverLimit(ped, isOverLimit);

    /// <summary>
    /// Pedが薬物中毒かどうか指定
    /// </summary>
    /// <param name="ped">対象者</param>
    /// <param name="isUnderInfluence">薬物中毒かどうか</param>
    internal static void SetPedUnderDrugsInfluence(Ped ped, bool isUnderInfluence)
    => STPFunctions.setPedUnderDrugsInfluence(ped, isUnderInfluence);

    /// <summary>
    /// 車両の保険の状態を指定
    /// </summary>
    /// <param name="vehicle">対象車両</param>
    /// <param name="status">状態</param>
    internal static void SetVehicleInsuranceStatus(Vehicle vehicle, STPVehicleStatus status)
    => STPFunctions.setVehicleInsuranceStatus(vehicle, status);

    /// <summary>
    /// 車両の登録状態を指定
    /// </summary>
    /// <param name="vehicle">対象車両</param>
    /// <param name="status">状態</param>
    private static void SetVehicleRegistrationStatus(Vehicle vehicle, STPVehicleStatus status)
    => STPFunctions.setVehicleRegistrationStatus(vehicle, status);
}