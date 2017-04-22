namespace CrystalEditor.ViewModels
{
	public sealed class StringLookupItemViewModel : ViewModelBase
	{
		public StringLookupItemViewModel(string key, string value)
		{
			m_key = key;
			m_value = value;
		}

		public string Key
		{
			get
			{
				VerifyAccess();
				return m_key;
			}
			set
			{
				SetPropertyField(nameof(Key), value, ref m_key);
			}
		}

		public string Value
		{
			get
			{
				VerifyAccess();
				return m_value;
			}
			set
			{
				SetPropertyField(nameof(Value), value, ref m_value);
			}
		}

		string m_key;
		string m_value;
	}
}
