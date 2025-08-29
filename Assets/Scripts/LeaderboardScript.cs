using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;

        // ------------------------------------------------------------
        private int highScore;
        private string userName;
        // ------------------------------------------------------------

        private void Start()
        {
            highScore = PlayerPrefs.GetInt("Highscore", 0);
            userName = PlayerPrefs.GetString("Name", "nullName");
            LoadEntries();
        }

        private void LoadEntries()
        {

            Leaderboards.Cookie.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    _entryTextObjects[i].text = $"{i+1})      {entries[i].Username}    -     {entries[i].Score}";
                    Debug.Log(_entryTextObjects[i].text);
                }
            });
        }

        public void UploadEntry()
        {
            Leaderboards.Cookie.UploadNewEntry(userName, highScore, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries();
            });
        }
    }
}
