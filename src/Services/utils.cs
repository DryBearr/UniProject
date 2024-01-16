namespace Services;

public static class Utils
{
    /// <summary>
    /// Generates a hashed password (:D just shuffle).
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password, or the original input if it's already hashed.</returns>

    public static string GetPasswordHash(string password) 
    {
        List<char> chars = password.ToList();
        int size = chars.Count();
        Random ran = new Random();
        string shuffled = string.Join("",chars);

        while(shuffled == password)
        {
            for(int i = 0; i < size; i ++)
            {
                int j = ran.Next(i, size);
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }
            shuffled = string.Join("",chars);
        }

        return shuffled;    
    }
}
   
    

