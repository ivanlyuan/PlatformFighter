using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MatchListPanel : MonoBehaviour
{
    public static MatchListPanel Instance;


	[SerializeField]
    private JoinButton joinButtonPrefab;

	private void Awake()
	{
        Instance = this;
		AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
	}

    private void OnEnable()
    {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void OnDisable()
    {
        AvailableMatchesList.OnAvailableMatchesChanged -= AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
	{
        if (Instance == null)
        {
            return;
        }
        ClearExistingButtons();
		CreateNewJoinGameButtons(matches);
	}

	private void ClearExistingButtons()
	{
		var buttons = GetComponentsInChildren<JoinButton>();
		foreach (var button in buttons)
		{
			Destroy(button.gameObject);
		}
	}

	private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches)
	{
		foreach (var match in matches)
		{
			var button = Instantiate(joinButtonPrefab);
			button.Initialize(match, transform);
		}
	}
}