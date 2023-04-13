﻿// Copyright (c) Microsoft. All rights reserved.

namespace ClientApp.Components;

public sealed partial class SettingsPanel : IDisposable
{
    private Approach _approach = Approach.RetrieveThenRead;
    private bool _open;
    private SupportedSettings _supportedSettings;

    [Inject] public required IStringLocalizer<SettingsPanel> Localizer { get; set; }
    [Inject] public required NavigationManager Nav { get; set; }

    public RequestOverrides Overrides { get; } = new();

    [Parameter]
#pragma warning disable BL0007 // Component parameters should be auto properties
    public bool Open
#pragma warning restore BL0007 // This is required for proper event propagation
    {
        get => _open;
        set
        {
            if (_open == value)
            {
                return;
            }

            _open = value;
            OpenChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<bool> OpenChanged { get; set; }

    private string Title => Localizer[nameof(Title)];
    private string ApproachLabel => Localizer[nameof(ApproachLabel)];
    private string RetrieveThenRead => Localizer[nameof(RetrieveThenRead)];
    private string ReadRetrieveRead => Localizer[nameof(ReadRetrieveRead)];
    private string ReadDecomposeAsk => Localizer[nameof(ReadDecomposeAsk)];
    private string OverridePromptTemplate => Localizer[nameof(OverridePromptTemplate)];
    private string RetrieveNumberOfDocs => Localizer[nameof(RetrieveNumberOfDocs)];
    private string ExcludeCategory => Localizer[nameof(ExcludeCategory)];
    private string UseSemanticRanker => Localizer[nameof(UseSemanticRanker)];
    private string UseQueryContext => Localizer[nameof(UseQueryContext)];
    private string SuggestFollowups => Localizer[nameof(SuggestFollowups)];
    private string Close => Localizer[nameof(Close)];

    protected override void OnInitialized() => Nav.LocationChanged += HandleLocationChanged;

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var url = new Uri(e.Location);
        var route = url.Segments.LastOrDefault();
        _supportedSettings = route switch
        {
            "ask" => SupportedSettings.Ask,
            "chat" => SupportedSettings.Chat,
            _ => SupportedSettings.All
        };
    }

    public void Dispose() => Nav.LocationChanged -= HandleLocationChanged;
}

public enum SupportedSettings
{
    All,
    Chat,
    Ask
};
