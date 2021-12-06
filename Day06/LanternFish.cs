namespace Day06
{
    internal class LanternFish
    {
        private int age;

        public LanternFish(int age)
        {
            this.age = age;
        }

        public bool IsSpawning { get; internal set; }

        internal void StepAge()
        {
            age--;
            IsSpawning = age < 0;
            if (IsSpawning) age = 6;
        }

        public override string ToString()
        {
            var spawnText = IsSpawning ? "S" : "";
            return $"age: {age} {spawnText}";
        }
    }
}