namespace WebSiteAPI.Application.Configurations
{
    public class JwtSettings
    {
        /// <summary>
        /// JWT için kullanılan gizli anahtar (Secret Key)
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token sağlayıcı (Issuer)
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token tüketicisi (Audience)
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Access Token geçerlilik süresi (dakika cinsinden)
        /// </summary>
        public int AccessTokenExpirationMinutes { get; set; }

        /// <summary>
        /// Refresh Token geçerlilik süresi (gün cinsinden)
        /// </summary>
        public int RefreshTokenExpirationDays { get; set; }
    }
}
