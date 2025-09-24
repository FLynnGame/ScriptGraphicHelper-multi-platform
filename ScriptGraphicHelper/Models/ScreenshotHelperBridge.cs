using ScriptGraphicHelper.Models.ScreenshotHelpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ScriptGraphicHelper.Models
{
    public enum LinkState
    {
        None = -1,
        Waiting = 0,
        Starting = 1,
        success = 2
    }

    public static class ScreenshotHelperBridge
    {
        public static LinkState State { get; set; } = LinkState.None;
        public static ObservableCollection<string> Result { get; set; } = new ObservableCollection<string>();

        // 产生的链接
        // [0] = {[0, 192.168.110.169:39947]}
        public static List<KeyValuePair<int, string>> Info { get; set; } = new List<KeyValuePair<int, string>>();

        // 选择的列表，选中对应的Select下标就是多少 Adb连接，值就为5
        //雷电模拟器3.0
        //雷电模拟器4.0
        //雷电模拟器64
        //雷神模拟器
        //大漠句柄
        //Adb连接
        //AJ连接
        //AT连接
        public static int Select { get; set; } = -1;

        // 产生的第几个链接？
        private static int _index = -1;
        public static int Index
        {
            get => _index;
            set
            {
                if (value != -1)
                {
                    _index = Info[value].Key;
                }
            }
        }

        // Init函数，默认返回所有的连接方式
        public static List<BaseHelper> Helpers = new();
        public static ObservableCollection<string> Init()
        {
            Helpers = new List<BaseHelper>();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Helpers.Add(new LdEmulatorHelper(0));
                Helpers.Add(new LdEmulatorHelper(1));
                Helpers.Add(new LdEmulatorHelper(2));
                Helpers.Add(new LdEmulatorHelper(3));
                try
                {
                    Helpers.Add(new HwndHelper());
                }
                catch { }
                Helpers.Add(new AdbHelper());
            }

            Helpers.Add(new AJHelper());
            Helpers.Add(new ATHelper());

            Result = new ObservableCollection<string>();

            foreach (var emulator in Helpers)
            {
                if (emulator.Path != string.Empty && emulator.Path != "")
                {
                    Result.Add(emulator.Name);
                }
            }
            State = LinkState.Waiting;
            return Result;

        }
        public static void Dispose()
        {
            try
            {
                foreach (var emulator in Helpers)
                {
                    emulator.Close();
                }
                Result.Clear();
                Info.Clear();
                Helpers.Clear();
                Select = -1;
                State = LinkState.None;
            }
            catch { }

        }
        public static void Changed(int index)
        {
            if (index >= 0)
            {
                for (var i = 0; i < Helpers.Count; i++)
                {
                    if (Helpers[i].Name == Result[index])
                    {
                        Select = i;
                        State = LinkState.Starting;
                    }
                }
            }
            else
            {
                Select = -1;
                State = LinkState.Starting;
            }
        }

        // 返回连接串信息
        public static async Task<ObservableCollection<string>> StartConnect()
        {
            ObservableCollection<string> result = new();
            Info = await Helpers[Select].Initialize();
            foreach (var item in Info)
            {
                result.Add(item.Value);
            }
            return result;
        }
        public static void ScreenShot()
        {
            Helpers[Select].ScreenShot(Index);
        }
    }
}
