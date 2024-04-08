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

using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using StationNaming.Setting;
using StationNaming.System;

namespace StationNaming
{
    public class Mod : IMod
    {
        public const string Name = "RollW_StationNaming";

        private static Mod _instance;

        private static readonly ILog LOG = LogManager.GetLogger($"{Name}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);

        private StationNamingSettings _settings;

        public void OnLoad(UpdateSystem updateSystem)
        {
            _instance = this;

            LOG.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                LOG.Info($"Current mod asset at {asset.path}");
            }

            _settings = new StationNamingSettings(this);
            _settings.RegisterInOptionsUI();
            Localizations.LoadTranslations();

            AssetDatabase.global.LoadSettings(
                Name, _settings, new StationNamingSettings(this)
            );

            LOG.Info($"Load settings: {_settings.ToJSONString()}");

            updateSystem.UpdateAt<UIBindingSystem>(SystemUpdatePhase.UIUpdate);
            updateSystem.UpdateAt<StationNamingSystem>(SystemUpdatePhase.UIUpdate);
            updateSystem.UpdateAt<TransportStopNamingSystem>(SystemUpdatePhase.UIUpdate);
        }

        public void OnDispose()
        {
            LOG.Info(nameof(OnDispose));
            _instance = null;
        }

        public StationNamingSettings GetSettings() => _settings;

        public static ILog GetLogger() => LOG;

        public static Mod GetInstance() => _instance;
    }
}