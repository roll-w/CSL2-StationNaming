import {Entity} from "cs2/input";

export namespace StationNaming {
    export const ModName = "RollW_StationNaming";

    export type NameCandidate = {
        name: string;
        source: string;
        refer: Entity;
        direction: string;
        edgeType: string;
    }

    export type BaseSerialized<T> = {
        value__: T;
        __Type: string;
    }

    // StationNaming.System.Direction
    export enum Direction {
        Init = 0,
        Start,
        End
    }

    export const directionToString = (direction: Direction) => {
        switch (direction) {
            case Direction.Init:
                return "Init";
            case Direction.Start:
                return "Start";
            case Direction.End:
                return "End";
            default:
                return "Unknown";
        }
    }

    // StationNaming.System.EdgeType
    export enum EdgeType {
        Same = 0,
        Other
    }

    export const edgeTypeToString = (edgeType: EdgeType) => {
        switch (edgeType) {
            case EdgeType.Same:
                return "Same";
            case EdgeType.Other:
                return "Other";
            default:
                return "Unknown";
        }
    }

    // StationNaming.System.NameSource
    export enum NameSource {
        Road = 0,
        Intersection,
        Owner,
        TransportStation,
        TransportDepot,
        ZoneBuilding,
        SignatureBuilding,
        School,
        FireStation,
        PoliceStation,
        Hospital,
        Park,

        /**
         * Other city service buildings not listed
         */
        CityService,

        /**
         * Other buildings that we don't know its type.
         */
        Building,
        Unknown
    }

    export const nameSourceToString = (nameSource: NameSource) => {
        switch (nameSource) {
            case NameSource.Road:
                return "Road";
            case NameSource.Intersection:
                return "Intersection";
            case NameSource.Owner:
                return "Owner";
            case NameSource.TransportStation:
                return "TransportStation";
            case NameSource.TransportDepot:
                return "TransportDepot";
            case NameSource.ZoneBuilding:
                return "ZoneBuilding";
            case NameSource.SignatureBuilding:
                return "SignatureBuilding";
            case NameSource.School:
                return "School";
            case NameSource.FireStation:
                return "FireStation";
            case NameSource.PoliceStation:
                return "PoliceStation";
            case NameSource.Hospital:
                return "Hospital";
            case NameSource.Park:
                return "Park";
            case NameSource.CityService:
                return "CityService";
            case NameSource.Building:
                return "Building";
            case NameSource.Unknown:
            default:
                return "Unknown";
        }
    }

    export type SerializedEntity = {
        Index: number;
        Version: number;
        __Type: string;
    }

    export const toEntity = (entity: SerializedEntity): Entity => {
        return {
            index: entity.Index,
            version: entity.Version
        }
    }

    export type SerializedNameCandidate = {
        Name: string;
        Source: BaseSerialized<NameSource>;
        Refer: SerializedEntity;
        Direction: BaseSerialized<Direction>;
        EdgeType: BaseSerialized<EdgeType>;
    }

    export const toNameCandidate = (candidate: SerializedNameCandidate): NameCandidate => {
        return {
            name: candidate.Name,
            source: nameSourceToString(candidate.Source.value__),
            refer: toEntity(candidate.Refer),
            direction: directionToString(candidate.Direction.value__),
            edgeType: edgeTypeToString(candidate.EdgeType.value__)
        }
    }

    export const getTranslationKeyOf = (key: string, type = '') => {
        if (!type || type === '') {
            return `StationNaming.${key}`;
        }

        return `StationNaming.${type}[${key}]`;
    }
}