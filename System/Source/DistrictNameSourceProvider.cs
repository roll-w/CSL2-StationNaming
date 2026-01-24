// MIT License
// 
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

using System.Collections.Generic;
using Game.Areas;
using Game.Common;
using Game.Objects;
using Game.UI;
using Unity.Entities;

namespace StationNaming.System.Source
{
    public class DistrictNameSourceProvider : INameSourceProvider
    {
        public ICollection<SourcedName> GetName(World world, EntityManager manager, NameSystem nameSystem,
            Entity contextEntity,
            NameSource nameSource)
        {
            var districtEntity = GetDistrictEntity(contextEntity, manager);
            if (Entity.Null == districtEntity)
            {
                return new List<SourcedName>();
            }

            var districtName = nameSystem.GetRenderedLabelName(districtEntity);
            return new[] { new SourcedName(districtName, districtEntity, NameSource.District) };
        }

        public bool Supports(NameSource nameSource)
        {
            return nameSource == NameSource.District;
        }

        private static Entity GetDistrictEntity(
            Entity entity,
            EntityManager entityManager
        )
        {
            if (entityManager.HasComponent<CurrentDistrict>(entity))
            {
                return entityManager.GetComponentData<CurrentDistrict>(entity).m_District;
            }

            if (entityManager.HasComponent<Owner>(entity))
            {
                return GetDistrictEntity(
                    StopNameHelper.RetrieveOwner(entityManager, entity),
                    entityManager
                );
            }

            if (!entityManager.HasComponent<Attached>(entity))
            {
                return Entity.Null;
            }

            var attached = entityManager.GetComponentData<Attached>(entity);
            var attachedParent = attached.m_Parent;

            if (!entityManager.HasComponent<BorderDistrict>(attachedParent))
            {
                return Entity.Null;
            }

            var borderDistrict = entityManager.GetComponentData<BorderDistrict>(attachedParent);
            return borderDistrict.m_Left != Entity.Null
                ? borderDistrict.m_Left
                : borderDistrict.m_Right;
        }
    }
}