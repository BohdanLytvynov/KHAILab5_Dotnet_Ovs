﻿using DAL.RepositoryWrapper;
using ElectionApp.ViewModels.Base.VM;
using ElectionApp.ViewModels.Models;
using System.Collections.ObjectModel;
using AutoMapper;
using System.Windows.Input;
using ElectionApp.ViewModels.Base.Commands;
using ElectionApp.WindowManagers;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using ElectionApp.Enums;
using ElectionApp.Views;
using System.Windows;

namespace ElectionApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        private string m_title;

        IRepositoryWrapper m_repWrapper;

        IWindowManager m_windowManager;

        IMapper m_mapper;

        private ObservableCollection<VoterViewModel> m_Voters;

        private int m_selectedVoterIndex;
        
        #endregion

        #region Properties

        public string Title 
        {
            get=> m_title;
            set=>Set(ref m_title, value);
        }

        public ObservableCollection<VoterViewModel> Voters 
        { 
            get=> m_Voters;
            set=> m_Voters = value;
        }

        public int SelectedVoterIndex 
        {
            get=> m_selectedVoterIndex;
            set=> Set(ref m_selectedVoterIndex, value); 
        }

        #endregion

        #region Commands
        public ICommand OnLoadDataButtonPressed { get; }

        public ICommand OnSaveButtonPressed { get; }

        public ICommand OnAddButtonPressed { get; }

        public ICommand OnEditButtonPressed { get; }

        public ICommand OnRemoveButtonPressed { get; }

        public ICommand OnGenerateReportPressed { get; }

        public ICommand OnAboutButtonPressed { get; }
        #endregion

        #region Ctor

        public MainWindowViewModel(IRepositoryWrapper repositoryWrapper, 
            IMapper mapper, IWindowManager windowManager) : this()
        {
            m_repWrapper = repositoryWrapper;

            m_mapper = mapper;

            m_windowManager = windowManager;

            var votersData = m_repWrapper.VoterRepository.GetAll();

            m_Voters = new ObservableCollection<VoterViewModel>();

            UpdateVotersFromDataBase(votersData, Voters, mapper);
        }

        public MainWindowViewModel() : base(0)
        {
            #region Init Fields
            m_title = "Election App V1";

            Voters = new ObservableCollection<VoterViewModel>();

            m_selectedVoterIndex = -1;

            #endregion

            #region Init Commands
            OnLoadDataButtonPressed = new Command(
                OnLoadDataButtonPressedExecute,
                CanOnLoadDataButtonPressedExecute);

            OnSaveButtonPressed = new Command(
                OnSaveButtonPressedExecute,
                CanOnSaveButtonPressedExecute
                );

            OnAddButtonPressed = new Command(
                OnAddButtonPressedExecute,
                CanOnAddButtonPressedExecute);

            OnEditButtonPressed = new Command(
                OnEditButtonButtonPressedExecute, 
                CanOnEditButtonPressedExecute);

            OnRemoveButtonPressed = new Command(
                OnRemoveButtonPressedExecute,
                CanOnRemoveButtonPressedExecute);

            OnGenerateReportPressed = new Command(
                OnGenerateReportButtonPressedExecute, 
                CanOnGenerateReportButtonPressedExecute
                );

            OnAboutButtonPressed = new Command(
                OnAboutButtonPressedExecute, 
                CanOnAboutButtonPressedExecute
                );
            #endregion
        }

        #endregion

        #region Methods

        #region On Load Data Button Pressed
        private bool CanOnLoadDataButtonPressedExecute(object p) => true;

        private void OnLoadDataButtonPressedExecute(object p)
        { 
            m_repWrapper.LoadData();

            var voters = m_repWrapper.VoterRepository.GetAll();

            if(Voters.Count > 0)
                Voters.Clear();

            UpdateVotersFromDataBase(voters, Voters, m_mapper);
        }
        #endregion

        #region On Save Button Pressed 

        private bool CanOnSaveButtonPressedExecute(object p) => Voters.Count > 0 && AllVotersAreValid(Voters);

        private void OnSaveButtonPressedExecute(object p)
        {
            var repo = m_repWrapper.VoterRepository;

            foreach (VoterViewModel v in Voters)
            {
                switch (v.Action)
                {
                    case Action.Add:
                        repo.Add(m_mapper.Map<Voter>(v));
                        v.Action = Action.None;
                        break;
                    case Action.Edit:
                        repo.Edit(v.Id, m_mapper.Map<Voter>(v));
                        v.Action = Action.None;
                        break;
                    case Action.Delete:
                        repo.Remove(v.Id);
                        Voters.Remove(v);
                        break;
                }
            }

            repo.SaveData();
        }

        #endregion

        #region On Add Button Pressed

        private bool CanOnAddButtonPressedExecute(object p) => true;

        private void OnAddButtonPressedExecute(object p)
        {
            Voters.Add(new VoterViewModel() { Action = Action.Add });

            UpdateVotersShowNumbers(Voters);
        }

        #endregion

        #region On Edit Button Pressed

        private bool CanOnEditButtonPressedExecute(object p)
        {
            return SelectedVoterIndex >= 0;
        }

        private void OnEditButtonButtonPressedExecute(object p)
        {
            Voters[SelectedVoterIndex].Action = Action.Edit;
        }

        #endregion

        #region On Remove Button Pressed

        private bool CanOnRemoveButtonPressedExecute(object p)
        {
            return SelectedVoterIndex >= 0 && Voters[SelectedVoterIndex].Action != Action.Add;
        }

        private void OnRemoveButtonPressedExecute(object p)
        {
            Voters[SelectedVoterIndex].Action = Action.Delete;
        }

        #endregion

        #region On Generate Report Button Pressed

        private bool CanOnGenerateReportButtonPressedExecute(object  p) => Voters.Count > 0;

        private void OnGenerateReportButtonPressedExecute(object p)
        {
            m_windowManager.OpenWindow(typeof(ReporterWindow));
        }

        #endregion

        #region On About Button Pressed

        private bool CanOnAboutButtonPressedExecute(object p) => true;

        private void OnAboutButtonPressedExecute(object p)
        {
            MessageBox.Show("Литивнов Богда Юрійович Гр 125 Варіант 9\n" +
                "У передвиборчій кампанії проводиться реєстрація кандидатів у депутати.\n" +
                " Кожен кандидат, подаючи заяву на реєстрацію, зазначає: ПІБ, номер округу, \n" +
                "в якому він збирається балотуватися, найменування партії, \n" +
                "яку він представляє, свій вік та професію. Прес-служба центральної \n" +
                "виборчої комісії видає інформаційний бюлетень, у якому наводить таку \n" +
                "інформацію: кількість поданих заяв на реєстрацію кандидатів кожної політичної партії; \n" +
                "середній вік кандидатів від кожної політичної партії; професія, що найчастіше зустрічається для кандидатів по кожній партії. \n" +
                "Вивести ці дані до окремої таблиці\n", Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        private static void UpdateVotersFromDataBase(IEnumerable<Voter> src, ObservableCollection<VoterViewModel> dest,
            IMapper mapper)
        {
            int i = 1;
            foreach (var voter in src)
            {
                var vm = mapper.Map<VoterViewModel>(voter);
                vm.Action = Action.None;
                vm.IsValid = true;
                vm.ShowNumber = i;
                dest.Add(vm);
                i++;
            }
        }

        private static void UpdateVotersShowNumbers(ObservableCollection<VoterViewModel> voters)
        { 
            int length = voters.Count;

            for (int i = 0; i < length; i++)
            {
                voters[i].ShowNumber = i + 1;
            }
        }

        private static bool AllVotersAreValid(ObservableCollection<VoterViewModel> voters)
        {
            foreach (var voter in voters)
            { 
                if(!voter.IsValid)
                    return false;
            }

            return true;
        }

        #endregion
    }
}
