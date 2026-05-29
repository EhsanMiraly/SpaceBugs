using UnityEngine.UIElements;



public class ListViewController
{
    public ListView listView;

    public ListViewController(ListView listView)
    {
        this.listView = listView;

        listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

        listView.selectionType = SelectionType.None;

        ScrollView scrollView = listView.Q<ScrollView>();

        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        scrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Elastic;

        scrollView.style.marginTop = 0;
        scrollView.style.marginRight = 0;
        scrollView.style.marginBottom = 0;
        scrollView.style.marginLeft = 0;

        scrollView.style.paddingTop = 0;
        scrollView.style.paddingRight = 0;
        scrollView.style.paddingBottom = 0;
        scrollView.style.paddingLeft = 0;
    }

}
