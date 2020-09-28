using UnityEditor;

namespace Kogane.Internal
{
	/// <summary>
	/// Project Settings における設定画面を管理するクラス
	/// </summary>
	internal sealed class AllSceneGlobalIlluminationDisablerSettingsProvider : SettingsProvider
	{
		//================================================================================
		// 変数
		//================================================================================
		private AllSceneGlobalIlluminationDisablerSettings m_settings;

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		internal AllSceneGlobalIlluminationDisablerSettingsProvider( string path, SettingsScope scope )
			: base( path, scope )
		{
		}

		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		public override void OnGUI( string searchContext )
		{
			if ( m_settings == null )
			{
				m_settings = AllSceneGlobalIlluminationDisablerSettings.Load();
			}

			var serializedObject        = new SerializedObject( m_settings );
			var scenePathFilterProperty = serializedObject.FindProperty( "m_scenePathFilter" );

			using ( var checkScope = new EditorGUI.ChangeCheckScope() )
			{
				EditorGUILayout.PropertyField( scenePathFilterProperty );

				if ( checkScope.changed )
				{
					serializedObject.ApplyModifiedProperties();
					AllSceneGlobalIlluminationDisablerSettings.Save();
				}
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// Project Settings にメニューを追加します
		/// </summary>
		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path     = "Kogane/UniAllSceneGlobalIlluminationDisabler";
			var provider = new AllSceneGlobalIlluminationDisablerSettingsProvider( path, SettingsScope.Project );

			return provider;
		}
	}
}