using System;

namespace Util.Operation
{
    /*
     * 本类提供位运算，指针等效率更高的运算技巧
     *
     * bit and & 都是1则为1，否则为0
     * bit not ~ 将1变0，将0变1
     * bit or  | 有1则1，否则为0
     * bit xor ^ 相同得0，不同得1
     */
    public static class OperationUtil
    {
        /*
         * &运算的应用
         * 1.取值。将n和一个所有位全部为1的数做运算结果不变 n & 1111111111111 = n
         * 也可以11110000，这样就忽略了后面4位，比如只取后8位   n & 0xff
         */
        
        
        /// 判断是否偶数
        public static bool IsEvenNumber(int n)
        {
            return (n & 1) == 0;
        }

        public static bool IsPowerOf2(int n)
        {
            return (n & (n - 1)) == 0;
        }

        /// 取绝对值
        public static int Abs(int n)
        {
            var y = n >> 31;
            return (n ^ y) - y;//(n+y)^y
        }
        
        /// 以方便查看的形式打印出二进制数
        public static string FormatTo2Base(int n)
        {
            var s = Convert.ToString(n, 2);

            int index = 4;
            int max = s.Length;
            while (index < max)
            {
                s = s.Insert(index + index/4 - 1, "_");
                index += 4;
            }

            return s;
        }
        
        /// <summary> 确保 hashcode 大于 0x40000000 </summary>
        public static int GetRedDotHashCode(int code)
        {
            return (code & 0x3fffffff) | 0x40000000;
        }
    }
}