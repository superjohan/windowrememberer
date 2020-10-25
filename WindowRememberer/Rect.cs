using System.Runtime.InteropServices;

namespace WindowRememberer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public bool isValid
        {
            get
            {
                return width >= 0 && height >= 0;
            }
        }

        public int width
        {
            get
            {
                return right - left;
            }
            set
            {
                // expects left to have been set
                right = left + value;
            }
        }

        public int height
        {
            get
            {
                return bottom - top;
            }
            set
            {
                // expects top to have been set
                bottom = top + value;
            }
        }

        public void setInvalid()
        {
            width = -1;
            height = -1;
        }

        override public string ToString()
        {
            return "(x: " + left + ", y: " + top + ", width: " + width + ", height: " + height + ")";
        }
    }
}
