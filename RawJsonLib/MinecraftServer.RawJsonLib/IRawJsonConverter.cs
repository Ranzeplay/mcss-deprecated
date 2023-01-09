namespace MinecraftServer.RawJsonLib
{
    public interface IRawJsonConverter<T>
    {
        public T Deserialize(string rawJson);
    }
}