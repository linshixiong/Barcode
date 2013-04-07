using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Emdoor.Barcode
{
    public class TSCLib
    {
        /// <summary>
        /// 显示TSCLIB.dll版本号
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        /// <summary>
        /// 打开打印机
        /// </summary>
        /// <param name="printername">打印机名称</param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        /// <summary>
        /// 设置条码格式
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="type">条码类型</param>
        /// <param name="height">高度</param>
        /// <param name="readable">是否列印条码码文</param>
        /// <param name="rotation">旋转角度</param>
        /// <param name="narrow">窄比因子</param>
        /// <param name="wide">宽比因子</param>
        /// <param name="code">码文内容</param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        /// <summary>
        /// 关闭打印机
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        /// <summary>
        /// 下载pcx文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="image_name"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        /// <summary>
        /// 跳页
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        /// <summary>
        /// 设置纸张不回吐
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        /// <summary>
        /// 使用打印机内置字体打印文字
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="fonttype"></param>
        /// <param name="rotation"></param>
        /// <param name="xmul"></param>
        /// <param name="ymul"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="set">张数</param>
        /// <param name="copy">份数</param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="printercommand"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        /// <summary>
        /// 设置打印机
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <param name="density"></param>
        /// <param name="sensor"></param>
        /// <param name="vertical"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);
        /// <summary>
        /// 使用windows内置字体打印文字
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="fontheight"></param>
        /// <param name="rotation"></param>
        /// <param name="fontstyle"></param>
        /// <param name="fontunderline"></param>
        /// <param name="szFaceName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

    }

}
