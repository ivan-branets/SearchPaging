namespace SearchPaging
{
    class Entity
    {
        public Entity(int id, int value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", Id, Value);
        }
    }
}
