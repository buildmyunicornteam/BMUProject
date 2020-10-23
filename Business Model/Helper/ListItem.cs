namespace Business_Model.Helper
{
    public sealed class ListItem<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

    }

    public sealed class ListItem
    {
        public string Key { get; set; }

        public string Value { get; set; }

    }
}
