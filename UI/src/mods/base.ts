import {Entity} from "cs2/input";
import {bindValue, useValue} from "cs2/api";

export namespace StationNaming {
    export const ModName = "RollW_StationNaming";

    export const isShowCandidates$ = bindValue<boolean>(
        ModName, "IsShowCandidates", false);

    export const isShowCandidates = () => {
        return useValue(isShowCandidates$);
    }

    export type NameSourceRefer = {
        source: string;
        refer: Entity;
    }

    export type NameCandidate = {
        name: string;
        refers: NameSourceRefer[];
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

        /**
         * Other city service buildings not listed
         */
        CityService,

        /**
         * Other buildings that we don't know its type.
         */
        Building,
        Unknown,
        None
    }

    export const nameSourceToString = (nameSource: NameSource) => {
        switch (nameSource) {
            case NameSource.Road:
                return "Road";
            case NameSource.Intersection:
                return "Intersection";
            case NameSource.District:
                return "District";
            case NameSource.Owner:
                return "Owner";
            case NameSource.TransportStation:
                return "TransportStation";
            case NameSource.TransportDepot:
                return "TransportDepot";
            case NameSource.SpawnableBuilding:
                return "SpawnableBuilding";
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
            case NameSource.Electricity:
                return "Electricity";
            case NameSource.Water:
                return "Water";
            case NameSource.Sewage:
                return "Sewage";
            case NameSource.CityService:
                return "CityService";
            case NameSource.Building:
                return "Building";
            case NameSource.Unknown:
                return "Unknown";
            case NameSource.None:
            default:
                return "None";
        }
    }

    export type SerializedEntity = {
        Index: number;
        Version: number;
        __Type: string;
    }

    export type SerializedNameSourceRefer = {
        Source: BaseSerialized<NameSource>;
        Refer: SerializedEntity;
    }

    export type SerializedNameCandidate = {
        Name: string;
        Refers: SerializedNameSourceRefer[];
        Direction: BaseSerialized<Direction>;
        EdgeType: BaseSerialized<EdgeType>;
    }

    export const toEntity = (entity: SerializedEntity): Entity => {
        return {
            index: entity.Index,
            version: entity.Version
        }
    }

    export const toNameCandidate = (candidate: SerializedNameCandidate): NameCandidate => {
        return {
            name: candidate.Name,
            refers: candidate.Refers.map(ref => {
                return {
                    source: nameSourceToString(ref.Source.value__),
                    refer: toEntity(ref.Refer)
                }
            }),
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

    export const combineNameSource = (candidate: SerializedNameCandidate)
        : NameSource => {
        const refers = candidate.Refers;
        if (refers.length === 0) {
            return NameSource.None;
        }
        if (refers.length === 1) {
            return refers[0].Source.value__;
        }
        if (refers.length === 2) {
            let first = refers[0].Source.value__;
            let second = refers[1].Source.value__;
            if (first === NameSource.Road && second === NameSource.Road) {
                return NameSource.Intersection;
            }
            return second
        }
        return refers[refers.length - 1].Source.value__;
    }
}