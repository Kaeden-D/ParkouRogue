using UnityEngine;

public static class Items
{

    public static int Count { get; private set; } = 0;

    public struct ItemStat
    {
        private byte _count;

        public ItemStat(int count = 0)
        {
            _count = (byte)count;
        }

        public static ItemStat operator+(ItemStat stat, int count)
        {
            stat._count += (byte)count;
            Count += count;
            return stat;
        }

        public static ItemStat operator-(ItemStat stat, int count)
        {
            stat._count -= (byte)count;
            Count -= count;
            return stat;
        }

        public static implicit operator byte(ItemStat stat)
        {
            return stat._count;
        }

        public override string ToString()
        {
            return _count.ToString();
        }
    }

    public static ItemStat GravUp;
    public static ItemStat GravDown;
    public static ItemStat SpeedUp;


}
