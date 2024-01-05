namespace Game_Systems.Structs {
    
    
    public struct Island {
        
        public int BaseSeedCoordinate;
        public int MidSeedCoordinate;
        public int SkySeedCoordinate;

        public Island(int baseSeed, int baseDimensions, int midSeed, int skySeed) {
            BaseSeedCoordinate = baseSeed;
            MidSeedCoordinate = midSeed;
            SkySeedCoordinate = skySeed;
        }
    }
}
