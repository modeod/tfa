namespace Domain.JobTitle
{
    public class JobTitle // AggregateRootEntity ???
    {
        public Guid Id { get; private set; } // could be replaced with ValueType JobTitleId if needed
        public string Name { get; private set; }
        public bool IsDeleted { get; private set; }

        public JobTitle(string name)
        {
            Id = Guid.NewGuid(); // or check for total uniqueness
            Name = name;
            IsDeleted = false;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
