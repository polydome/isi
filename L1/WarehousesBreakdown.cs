namespace L1
{
    public class WarehousesBreakdown
    {
        private int activeWarehousesCount;
        private int inactiveWarehousesCount;

        public WarehousesBreakdown(int activeWarehousesCount, int inactiveWarehousesCount)
        {
            this.activeWarehousesCount = activeWarehousesCount;
            this.inactiveWarehousesCount = inactiveWarehousesCount;
        }

        public int ActiveWarehousesCount => activeWarehousesCount;

        public int InactiveWarehousesCount => inactiveWarehousesCount;
    }
}