using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace BasicCRUD
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo loggingFile;
        ObservableCollection<Session> sessions;
        private SqlConnectionStringBuilder connectionStringBuilder;
        private string connectionString;
        private SqlConnection databaseConnection;
        Binding bindingSessionDataGrid;
        DispatcherTimer mainTicker;
        TimerClock timerClock;
        public MainWindow()
        {
            try
            {
                loggingFile = new FileInfo("logs.txt");
                InitializeComponent();
                this.DataContext = this;
                sessions = new ObservableCollection<Session>();
                databaseConnection = DatabaseConnect();
                //TIMER setting
                mainTicker = new DispatcherTimer();
                mainTicker.Interval = new TimeSpan(0, 0, 0, 0, 42);
                mainTicker.Tick += TimerClock_Tick;
                mainTicker.Start();
                //
                //CLOCK setting
                timerClock = new TimerClock();
                //
                //BINDINGS
                bindingSessionDataGrid = new Binding();
                bindingSessionDataGrid.Source = sessions;
                MainDataGrid.SetBinding(DataGrid.ItemsSourceProperty, bindingSessionDataGrid);
                //
                //Load sessions to Collection
                using (var communicator = new DatabaseCommunicator(DatabaseConnect()))
                {
                    try
                    {
                        foreach (Session sessionToAdd in communicator.LoadSessions())
                        {
                            sessions.Add(sessionToAdd);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(loggingFile, true, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(loggingFile, true, ex);
                return;
            }
        }
        private void TimerClock_Tick(object sender, EventArgs e)
        {
            DurationTime_TextBlock.Text = timerClock.Duration;
        }
        ~MainWindow()
        {
            //TODO dispose of the dtb connection
            //databaseConnection.Dispose();
        }
        private static SqlConnection DatabaseConnect()
        {
            try
            {
                var connectionStringBuilder = new SqlConnectionStringBuilder(@"Data Source = (LocalDB)\MSSQLLocalDB;
                AttachDbFilename = C:\Users\Eliss\source\repos\BasicCRUD\Database.mdf; Integrated Security = True");
                var connectionString = connectionStringBuilder.ConnectionString;
                var databaseConnection = new SqlConnection(connectionString);
                databaseConnection.Open();
                return databaseConnection;
            }
            catch
            {
                throw;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var toAdd = new Session(
                (Session.Subject)Enum.Parse(typeof(Session.Subject),
                Subject_TextBox.Text),
                float.Parse(Duration_TextBox.Text),
                DateTime.Now.Date
                );
            using (DatabaseCommunicator databaseCommunicator = new DatabaseCommunicator(DatabaseConnect()))
            {
                try
                {
                    if (databaseCommunicator.AddSession(toAdd))
                    {
                        sessions.Add(toAdd);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occured" + ex.ToString());
                }


            }

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timerClock.Start();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timerClock.Stop();
        }
    }
}
