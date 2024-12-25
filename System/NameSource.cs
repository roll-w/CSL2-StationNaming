// Copyright (c) 2024 RollW
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace StationNaming.System;

public enum NameSource : uint
{
    Owner = 0,
    Road,
    Intersection,
    District,
    TransportStation,
    TransportDepot,
    SpawnableBuilding,
    SignatureBuilding,
    School,
    FireStation,
    PoliceStation,
    Hospital,
    Park,
    Electricity,
    Water,
    Sewage,
    Admin,
    RoadFacility,
    Garbage,
    Disaster,
    Deathcare,
    Telecom,
    Post,
    Parking,

    /**
     * Other city service buildings not listed
     */
    CityService,

    /**
     * Other buildings that we don't know its type.
     */
    Building,
    Unknown,

    /**
     * Used for scope, not a real source.
     */
    None
}

public static class NameSourceExtensions
{
    public static bool CouldNaming(this NameSource source)
    {
        return source switch
        {
            NameSource.TransportStation
                or NameSource.TransportDepot
                or NameSource.FireStation
                or NameSource.PoliceStation
                or NameSource.School
                or NameSource.Hospital
                or NameSource.Park
                or NameSource.Electricity
                or NameSource.Water
                or NameSource.Sewage
                or NameSource.Admin
                or NameSource.RoadFacility
                or NameSource.Garbage
                or NameSource.Disaster
                or NameSource.Deathcare
                or NameSource.Telecom
                or NameSource.Post
                or NameSource.Parking
                or NameSource.CityService
                => true,
            _ => false
        };
    }

    public static bool IsBuilding(this NameSource source)
    {
        return source switch
        {
            NameSource.TransportStation
                or NameSource.TransportDepot
                or NameSource.FireStation
                or NameSource.PoliceStation
                or NameSource.School
                or NameSource.Hospital
                or NameSource.Park
                or NameSource.Electricity
                or NameSource.Water
                or NameSource.Sewage
                or NameSource.Admin
                or NameSource.RoadFacility
                or NameSource.Garbage
                or NameSource.Disaster
                or NameSource.Deathcare
                or NameSource.Telecom
                or NameSource.Post
                or NameSource.Parking
                or NameSource.CityService
                or NameSource.Building
                => true,
            _ => false
        };
    }
}