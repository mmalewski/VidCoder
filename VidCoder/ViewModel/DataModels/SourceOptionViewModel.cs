﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using VidCoder.Model;
using HandBrake.Interop;
using Microsoft.Practices.Unity;

namespace VidCoder.ViewModel
{
	public class SourceOptionViewModel : ViewModelBase
	{
		private SourceOption sourceOption;
		private ICommand chooseSourceCommand;

		public SourceOptionViewModel(SourceOption sourceOption)
		{
			this.sourceOption = sourceOption;
		}

		public string Image
		{
			get
			{
				switch (this.sourceOption.Type)
				{
					case SourceType.File:
						return "/Icons/video-file.png";
					case SourceType.VideoFolder:
						return "/Icons/folder.png";
					case SourceType.Dvd:
						if (this.sourceOption.DriveInfo.DiscType == DiscType.Dvd)
						{
							return "/Icons/disc.png";
						}
						else
						{
							return "/Icons/bludisc.png";
						}
					default:
						break;
				}

				return null;
			}
		}

		public string Text
		{
			get
			{
				switch (this.sourceOption.Type)
				{
					case SourceType.File:
						return "Video File...";
					case SourceType.VideoFolder:
						return "DVD/Blu-ray Folder...";
					case SourceType.Dvd:
						return this.sourceOption.DriveInfo.RootDirectory + " - " + this.sourceOption.DriveInfo.VolumeLabel;
					default:
						break;
				}

				return string.Empty;
			}
		}

		public string VolumeLabel
		{
			get
			{
				return this.sourceOption.DriveInfo.VolumeLabel;
			}

			set
			{
				this.sourceOption.DriveInfo.VolumeLabel = value;
				this.NotifyPropertyChanged("VolumeLabel");
				this.NotifyPropertyChanged("Text");
			}
		}

		public SourceOption SourceOption
		{
			get { return this.sourceOption; }
		}

		public ICommand ChooseSourceCommand
		{
			get
			{
				if (this.chooseSourceCommand == null)
				{
					this.chooseSourceCommand = new RelayCommand(param =>
					{
						var mainVM = Unity.Container.Resolve<MainViewModel>();

						switch (this.SourceOption.Type)
						{
							case SourceType.File:
								mainVM.SetSourceFromFile();
								break;
							case SourceType.VideoFolder:
								mainVM.SetSourceFromFolder();
								break;
							case SourceType.Dvd:
								mainVM.SetSourceFromDvd(this.SourceOption.DriveInfo);
								break;
							default:
								break;
						}
					});
				}

				return this.chooseSourceCommand;
			}
		}
	}
}
