public static class PasswordStorageDB
{
    private static List<byte[]> KnownPasswords = new List<byte[]>{
        new byte[] { 199, 154, 247, 34, 155, 204, 106, 79, 152, 183, 151, 189, 129, 76, 169, 20},
        new byte[] { 23, 214, 40, 178, 134, 28, 192, 212, 246, 62, 49, 114, 143, 35, 125, 126},
        new byte[] { 64, 43, 224, 109, 120, 229, 46, 162, 154, 15, 188, 44, 72, 165, 154, 41},
    };
    public static bool iKnowThisHash(byte[] hash)
    {
        return KnownPasswords.Exists(knowHash => hash.SequenceEqual(knowHash));
    }
}