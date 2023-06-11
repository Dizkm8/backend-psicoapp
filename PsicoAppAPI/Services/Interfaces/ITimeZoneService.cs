namespace PsicoAppAPI.Services.Interfaces
{
    /// <summary>
    /// This interface is used to encapsulate the utilities for
    /// UTC time conversion.
    /// </summary>
    public interface ITimeZoneService
    {
        /// <summary>
        /// This method converts a UTC time to Chilean time.
        /// </summary>
        /// <param name="dateTime">Datetime to convert</param>
        /// <returns>Converted Datetime or null if something went wrong</returns>
        public Task<DateTime?> ConvertToChileUTC(DateTime dateTime); 
    }
}