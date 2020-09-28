using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
	internal static class AllSceneGlobalIlluminationDisabler
	{
		private const string MENU_ITEM_ROOT = "Edit/UniAllSceneGlobalIlluminationDisabler/";

		private static readonly EditorDialog m_editorDialog = new EditorDialog( "UniAllSceneGlobalIlluminationDisabler" );

		[MenuItem( MENU_ITEM_ROOT + "すべてのシーンの Global Illumination を無効にする" )]
		private static void ExecuteAll()
		{
			if ( !m_editorDialog.OpenYesNo( "すべてのシーンの Global Illumination を無効化しますか？" ) ) return;

			SceneProcessor.ProcessAllScenes( OnProcess );

			m_editorDialog.OpenOk( "すべてのシーンの Global Illumination を無効化しました" );
		}

		[MenuItem( MENU_ITEM_ROOT + "Project Settings で設定されているシーンの Global Illumination を無効にする" )]
		private static void ExecuteBy()
		{
			if ( !m_editorDialog.OpenYesNo( "Project Settings で設定されているシーンの Global Illumination を無効化しますか？" ) ) return;

			var settings = AllSceneGlobalIlluminationDisablerSettings.Load();

			SceneProcessor.ProcessAllScenes
			(
				scenePathFilter: scenePath => settings.ScenePathFilter.Any( x => scenePath.StartsWith( x ) ),
				onProcess: OnProcess
			);

			m_editorDialog.OpenOk( "Project Settings で設定されているシーンの Global Illumination を無効化しました" );
		}

		private static SceneProcessResult OnProcess( Scene scene )
		{
			if ( !Lightmapping.TryGetLightingSettings( out _ ) ) return SceneProcessResult.NOT_CHANGE;
			Lightmapping.lightingSettings = null;
			return SceneProcessResult.CHANGE;
		}
	}
}