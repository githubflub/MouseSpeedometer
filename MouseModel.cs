using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseSpeedometer
{
    class MouseModel
    {
        // Data
        /**
         * RIDEV_INPUTSINK = 0x00000100
         * Used for the instantiation of a RAWINPUTDEVICE structure object. 
         * Means that the program will still receive data even if it's 
         * only running in the background. 
         */ 
        private const int RIDEV_INPUTSINK = 0x00000100;

        /**
         * Later in this program we will be expecting a 
         * message from ths OS, 0x00FF, which is known as 
         * WM_INPUT for simplicity. When we are checking to 
         * see if we've gotten that message, we will just use
         * WM_INPUT in our comparison, again for simplicity. 
         */ 
        private const int WM_INPUT = 0x00FF;

        /**
         * RID_INPUT
         * A command flag for the GetRawInputData() method
         * that specifices that I want the raw data and not
         * head information. 
         */ 
        private const int RID_INPUT = 0x10000003;

        private const int RIM_TYPEMOUSE = 0;
        private int cpi;
        private double max_speed;
        private double current_speed;

        /**
         * The following code has         * 
         * [StructLayout(LayoutKind.Sequential)]
         * applied to it, but it is a C# default and
         * doesn't actually need to be written, but it is
         * necessary to work with the Windows API, so it is 
         * good to write. Also, System.Runtime.InteropServices
         * is required to use this notation. 
         * 
         * internal is also a C# default, but I'm writing 
         * it anyway
         */

        /**
         * RAWINPUTDEVICE is struct 1 of 7 needed for this program. 
         * This is an "object" that will be a parameter
         * of the RegisterInputDevices() Windows API method
         * that we will soon call. The implementation of
         * this struct comes from Microsoft documentation.
         * 
         * This struct makes use of [MarshalAs()] statements.
         * These statements specify how to handle each variable. 
         * For example, ushort variables get a U2 designation, 
         * because ushorts are 16-bit unsigned integers.
         */          
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICE
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsagePage;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsage;
            /**
             * Microsoft specifies that dwFlags is of type DWORD
             * which maps to UInt32 in C#, which can be simplified
             * to uint. Because we are marshalling as U4, we can just
             * map DWORD to an int.
             */
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
            public IntPtr hwndTarget; /* C++ HWND -> IntPtr in C#*/ 
        }

        /**
         * RAWINPUTHEADER is struct 2 of 7 needed for this program. 
         * It is needed to meet the parameter requirements of GetRawInputData(). 
         */ 
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int wParam;
        }

        /** 
         * BUTTONSSTR is struct 5 of 7 needed for this program. 
         * It is used to make RAWMOUSE work, while RAWMOUSE
         * is being used to make RAWINPUT work. 
         */ 
        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTONSSTR
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonFlags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonData;
        }

        /**
         * RAWMOUSE is struct 4 of 7 needed for this program.
         * It is being created to make struct 3, RAWINPUT,
         * work. It requires that we make a BUTTONSSTR struct,
         * which you will find above.  
         */ 
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWMOUSE
        {
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)]
            public ushort usFlags;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)]
            public uint ulButtons;
            [FieldOffset(4)]
            public BUTTONSSTR buttonsStr;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(8)]
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(20)]
            public uint ulExtraInformation;
        }

        /**
         * RAWKEYBOARD is struct 6 of 7 needed for this program.
         * It is needed to make struct 3, RAWINPUT, work. 
         */ 
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWKEYBOARD
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort MakeCode;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Flags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Reserved;
            [MarshalAs(UnmanagedType.U2)]
            public ushort VKey;
            [MarshalAs(UnmanagedType.U4)]
            public uint Message;
            [MarshalAs(UnmanagedType.U4)]
            public uint ExtraInformation;
        }

        /**
         * RAWHID is struct 7 of 7 needed for this program. 
         * It is needed to make struct 3, RAWINPUT, work. 
         */ 
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWHID
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwSizHid;
            [MarshalAs(UnmanagedType.U4)]
            public int dwCount;
        }

        /** 
         * RAWINPUT is struct 3 of 7 needed for this program. 
         * RAWINPUT requires 3 structs that haven't been 
         * created yet: RAWMOUSE, RAWKEYBOARD, RAWHID. 
         * They will be created above. 
         * 
         */
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;
            [FieldOffset(16)]
            public RAWMOUSE mouse;
            [FieldOffset(16)]
            public RAWKEYBOARD keyboard;
            [FieldOffset(16)]
            public RAWHID hid;
        }

        /**
         * The operating system offers a services that send messages 
         * to your program. In this case, I want messages from the mouse. 
         * In order to receive those messages, I need to create a RAWINPUTDEVICE
         * within my program and then register it with the operating system
         * with the following RegisterRawInputDevices() method from 
         * a DLL that comes with Windows.         * 
         * 
         */
        [DllImport("user32.dll")]
        extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices,
                                                   uint uiNumDevices,
                                                   uint cbSize);
        /**
         * Each time I get a WM_INPUT method, I want to get some data, 
         * so I need this method as well. 
         */ 
        [DllImport("User32.dll")]
        extern static uint GetRawInputData(IntPtr hRawInput,
                                           uint uiCommand,
                                           IntPtr pData,
                                           ref uint pcbSize,
                                           uint cbSizeHeader);

        /**
         * We've imported the method to register our raw input device
         * now we perform the registration using this method. 
         * 
         * 1. Create a RAWINPUTDEVICE structure
         * 2. register it with the operating system by calling the imported method.
         */ 
        public void RegisterRawInputMouse(IntPtr hwnd)
        {
            /**
             * 1.
             * RegisterRawInputDevices() actually wants an array of 
             * devices as input, so we create an array of RAWINPUTDEVICE
             * structures, but we set its size to 1, because we only
             * care about 1 device: the mouse. 
             */             
            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
            rid[0].usUsagePage = 1;
            rid[0].usUsage = 2;
            rid[0].dwFlags = RIDEV_INPUTSINK;
            rid[0].hwndTarget = hwnd;

            // Attempt registration
            if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
            {
                // use of Debug requires System.Diagnostics
                Debug.WriteLine("RegisterRawInputDevices() Failed");
            }
            else
            {
                Console.WriteLine("Successful registration!");
            }
        }

        public delegate void MouseEventHandler(object Mouse, MouseEvent meventinfo); 
        public MouseEventHandler mevent; 

        /**
         * System.Windows.Forms required to use Messasge.
         */
        public void ProcessRawInput(Message m)
        {
            if (m.Msg == WM_INPUT)
            {
                /**
                 * We create a buffer to ___. 
                 * This buffer needs to be the correct size of the 
                 * mouse data that we wish to receive. We can leverage
                 * GetRawInputData() to figure out what size buffer we need.
                 * If we enter IntPtr.Zero (null) as its 3rd argument, then
                 * GetRawInputData() will return the required buffer size to 
                 * the 4th argument. 
                 * 
                 * We create dwSize to use as the 4th argument. 
                 */
                IntPtr buffer;                  
                uint dwSize = 0;

                // Leveraging GetRawInputData() to get the size for the buffer. 
                GetRawInputData(m.LParam,
                                RID_INPUT, 
                                IntPtr.Zero, // 3rd argument
                                ref dwSize,  // 4th argument
                                (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));                

                /** 
                 * We are allocating memory that we must remember to free later!
                 * The amount of memory allocated is based on the buffer size that 
                 * GetRawInputData() told us we needed. Thus, it's based on dwSize. 
                 */ 
                buffer = Marshal.AllocHGlobal((int)dwSize);
                try
                {
                    /**
                     * If the buffer has had memory allocated for it AND 
                     * if the number of bytes copied into the buffer is 
                     * equal to the bytes that we allocated for it, as 
                     * determined by dwSize.
                     */ 
                    if (buffer != IntPtr.Zero &&
                        GetRawInputData(m.LParam, 
                                        RID_INPUT, 
                                        buffer, 
                                        ref dwSize,
                                        (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                    {
                        /**
                         * From GeRawInputData(), we have retrieved a pointer to a 
                         * block of memory. We wish to create an object out
                         * of that block of memory so that we can work with it. 
                         * We use the Marshal.PtrToStructure() method, which 
                         * takes a pointer and returns an object of the specified
                         * type. 
                         */ 
                        RAWINPUT rawinput = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                        /**
                         * If the raw data is coming from the mouse
                         */ 
                        if (rawinput.header.dwType == RIM_TYPEMOUSE)
                        {
                            // Create a mouse event and fire it. 

                            // Create mouse event
                            MouseEvent meventinfo = new MouseEvent(rawinput.mouse.lLastX, rawinput.mouse.lLastY);

                            // Fire it 
                            mevent(this, meventinfo); 
                        }
                    }
                }
                finally
                {
                    // Here we are freeing the memory that we allocated.
                    Marshal.FreeHGlobal(buffer);
                }       
            }
        }

        // Constructor
        public MouseModel()
        {
            cpi = 0;
            max_speed = 0; 
        }

        // Functions
        public bool set_cpi(int cpi)
        {            
            if (cpi > 0)
            {
                this.cpi = cpi;
                return true;
            }
            this.cpi = 0;
            return false;             
        }

        public int get_cpi()
        {
            return cpi; 
        }

        public double get_current_speed()
        {
            return 0.0; 
        }

        public double get_max_speed()
        {
            return 0.0; 
        }
    }
}
