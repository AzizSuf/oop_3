// Задание 1. Написать с использованием конструкций Switch, Try, Catch метод анализа опасных состояний оборудования компьютера

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            logsRichTextBox.IsReadOnly = true;
            logsRichTextBox.Document.Blocks.Clear();
        }

        private void LogColored(string msg, SolidColorBrush color)
        {
            TextRange textRange = new TextRange(logsRichTextBox.Document.ContentEnd, logsRichTextBox.Document.ContentEnd);
            textRange.Text = msg + '\r';
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }

        private void LogMessage(string msg, SuccessFailureResult status)
        {
            switch (status)
            {
                case SuccessFailureResult.Success:
                    LogColored($"{msg}: Success", Brushes.Green);
                    break;
                case SuccessFailureResult.Fail:
                    LogColored($"{msg}: Fail", Brushes.Red);
                    break;
            }
        }

        private void LogMessage(string msg, CoolantSystemStatus status)
        {
            switch (status)
            {
                case CoolantSystemStatus.OK:
                    LogColored($"{msg}: OK", Brushes.Green);
                    break;
                case CoolantSystemStatus.Check:
                    LogColored($"{msg}: Check", Brushes.Blue);
                    break;
                case CoolantSystemStatus.Fail:
                    LogColored($"{msg}: Fail", Brushes.Red);
                    break;
            }
        }

        private void LogExceptionMessage(string msg)
        {
            LogColored(msg, Brushes.Red);
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs e)
        {
            logsRichTextBox.Document.Blocks.Clear();

            Switch sw = new Switch();
            int exceptionsCount = 0;
            int failCount = 0;

            // Step 1 - disconnect from the Power Generator
            try
            {
                SuccessFailureResult r = sw.DisconnectPowerGenerator();
                LogMessage("Step 1 - disconnect from the Power Generator", r);
                if (r == SuccessFailureResult.Fail)
                {
                    failCount++;
                }
            }
            catch (PowerGeneratorCommsException ex)
            {
                LogExceptionMessage($"*** Exception in step 1: {ex.Message}");
                exceptionsCount++;
            }


            // Step 2 - Verify the status of the Primary Coolant System
            try
            {
                CoolantSystemStatus s = sw.VerifyPrimaryCoolantSystem();
                LogMessage("Step 2 - Verify the status of the Primary Coolant System", s);
                if (s == CoolantSystemStatus.Fail)
                {
                    failCount++;
                }
            }
            catch (CoolantPressureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 2: {ex.Message}");
                exceptionsCount++;
            }
            catch (CoolantTemperatureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 2: {ex.Message}");
                exceptionsCount++;
            }


            // Step 3 - Verify the status of the Backup Coolant System
            try
            {
                CoolantSystemStatus s = sw.VerifyBackupCoolantSystem();
                LogMessage("Step 3 - Verify the status of the Backup Coolant System", s);
                if (s == CoolantSystemStatus.Fail)
                {
                    failCount++;
                }
            }
            catch (CoolantPressureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 3: {ex.Message}");
                exceptionsCount++;
            }
            catch (CoolantTemperatureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 3: {ex.Message}");
                exceptionsCount++;
            }


            // Step 4 - Record the core temperature prior to shutting down the reactor
            try
            {
                double temperature = sw.GetCoreTemperature();
                LogMessage("Step 4 - Record the core temperature prior to shutting down the reactor", SuccessFailureResult.Success);
            }
            catch (CoreTemperatureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 4: {ex.Message}");
                exceptionsCount++;
            }

            // Step 5 - Insert the control rods into the reactor
            try
            {
                SuccessFailureResult r = sw.InsertRodCluster();
                LogMessage("Step 5 - Insert the control rods into the reactor", r);
                if (r == SuccessFailureResult.Fail)
                {
                    failCount++;
                }
            }
            catch (RodClusterReleaseException ex)
            {
                LogExceptionMessage($"*** Exception in step 5: {ex.Message}");
                exceptionsCount++;
            }


            // Step 6 - Record the core temperature after shutting down the reactor
            try
            {
                double temperature = sw.GetCoreTemperature();
                LogMessage("Step 6 - Record the core temperature after shutting down the reactor", SuccessFailureResult.Success);
            }
            catch (CoreTemperatureReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 6: {ex.Message}");
                exceptionsCount++;
            }


            // Step 7 - Record the core radiation levels after shutting down the reactor
            try
            {
                double radiationLevel = sw.GetRadiationLevel();
                LogMessage("Step 7 - Record the core radiation levels after shutting down the reactor", SuccessFailureResult.Success);
            }
            catch (CoreRadiationLevelReadException ex)
            {
                LogExceptionMessage($"*** Exception in step 7: {ex.Message}");
                exceptionsCount++;
            }


            // Step 8 - Broadcast "Shutdown Complete" message
            try
            {
                sw.SignalShutdownComplete();
                LogMessage("Step 8 - Broadcast \"Shutdown Complete\" message", SuccessFailureResult.Success);
            }
            catch (SignallingException ex)
            {
                LogExceptionMessage($"*** Exception in step 8: {ex.Message}");
                exceptionsCount++;
            }


            // Conclusion
            if (exceptionsCount == 0 && failCount == 0)
            {
                LogColored("\r\r*** Shutdown successfully ***", Brushes.Green);
            }
            else
            {
                LogColored($"\r\r*** Shutdown failed! ***", Brushes.Red);

                if (exceptionsCount != 0)
                {
                    LogColored($"{exceptionsCount} exceptions occurred during shutdown", Brushes.Red);
                }
                if (failCount != 0)
                {
                    LogColored($"{failCount} fails occurred during shutdown", Brushes.Red);
                }
            }
        }
    }
}
