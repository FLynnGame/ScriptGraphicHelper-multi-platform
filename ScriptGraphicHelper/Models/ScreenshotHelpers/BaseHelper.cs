using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptGraphicHelper.Models.ScreenshotHelpers
{
    public abstract class BaseHelper
    {
        public abstract Action<Bitmap>? OnSuccessed { get; set; }
        public abstract Action<string>? OnFailed { get; set; }
        public abstract Action<string>? OnConnected { get; set; } // 连接成功
        public abstract Action<string>? OnConnectFailed { get; set; } // 连接失败
        public abstract string Path { get; }
        public abstract string Name { get; }
        public abstract bool IsStart(int Index);
        public abstract Task<List<KeyValuePair<int, string>>> Initialize();
        public abstract Task<List<KeyValuePair<int, string>>> GetList();
        public abstract void ScreenShot(int Index);
        public abstract void Close();
    }
}
