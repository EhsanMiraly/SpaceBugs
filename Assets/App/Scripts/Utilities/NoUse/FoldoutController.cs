using UnityEngine.UIElements;

public class FoldoutController
{

    public Foldout foldout;

    public FoldoutController()
    {
        foldout = new Foldout();
        foldout.value = false;
    }

    /*

        public Foldout MakeFoldoutForAllWords()
        {

            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;

                TemplateContainer templateContainer = AppCurrentData.buttonInFoldoutTemplate.Instantiate();
                Button button = templateContainer.Q<Button>();
                button.text = DBTablesNames.frequencyOfWords[i];
                button.name = DBTablesNames.frequencyOfWords[i];

                button.RegisterCallback<ClickEvent>(evt =>
                OnLengthAndFrequencySelectedToShowInWordDetails(foldout.name, index));

                foldout.AddToClassList("BorderDarkBlue");
                foldout.AddToClassList("RoundEdge30PX");

                foldout.Add(templateContainer);
            }

            return foldout;
        }


        public void OnLengthAndFrequencySelectedToShowInWordDetails(string foldoutName, int frequency)
        {
            AppCurrentData.userSelectedFrequencyOfWords = frequency;

            AppCurrentData.words = uIConnector.dataBaseManager.WordService.
                            GetWordsFromTableByFrequency(foldoutName.Replace(" ", ""),
                            AppCurrentData.userSelectedFrequencyOfWords).ToList<Word>();

            learnedOrNotPageManager.ShowWordsInLearnedOrNotListViews();

            uIConnector.SetPageActive(uIConnector.learnedOrNot_Page);
        }
        */
}
