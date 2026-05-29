using UnityEngine.UIElements;


public class ScrollViewController
{

    public static void InitializeScrollView(ScrollView scrollView)
    {
        scrollView.style.width = Length.Percent(100);
        scrollView.style.height = Length.Percent(100);

        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        scrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Elastic;
    }

}
