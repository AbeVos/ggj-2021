namespace Script
{
    public enum GameState
    {
        /// <summary>
        /// De default state, het menu in dit geval
        /// </summary>
        Init,
        /// <summary>
        /// De intro hoe je in je huidige situatie bent beland
        /// </summary>
        IntoSequence,
        /// <summary>
        /// Speler is bezig met de hoofd interactie van het spel
        /// </summary>
        GamePlay,
        /// <summary>
        /// De speler heeft succesvol de dialoog boom genavigeerd
        /// </summary>
        GameWon,
        /// <summary>
        /// De speler heeft te veel fouten gemaakt
        /// </summary>
        GameFailed
    }
}