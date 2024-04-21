namespace StationNaming.System;

public enum TargetType
{
    BusStop,
    BusStation,
    TrainStop,
    TrainStation,
    SubwayStop,
    SubwayStation,
    TramStop,
    TramStation,
    ShipStop,
    Harbor,
    AirplaneStop,
    Airport,
    TaxiStand,
    TaxiStation,
    Custom,
    None
}

public static class TargetTypeExtensions
{
    public static bool IsStop(this TargetType type)
    {
        return type switch
        {
            TargetType.BusStop => true,
            TargetType.TrainStop => true,
            TargetType.SubwayStop => true,
            TargetType.TramStop => true,
            TargetType.ShipStop => true,
            TargetType.AirplaneStop => true,
            TargetType.TaxiStand => true,
            _ => false
        };
    }

    public static bool IsStation(this TargetType type)
    {
        return type switch
        {
            TargetType.BusStation => true,
            TargetType.TrainStation => true,
            TargetType.SubwayStation => true,
            TargetType.TramStation => true,
            TargetType.Harbor => true,
            TargetType.Airport => true,
            TargetType.TaxiStation => true,
            _ => false
        };
    }

    public static bool IsTransport(this TargetType type)
    {
        return type.IsStop() || type.IsStation();
    }

}