using System;
using System.IO;

public class Logger
{
    #region Fields
    private static Logger _instance;

    public string FolderName = DateTime.Now.ToString("MM-dd-yy");
    public string FileName = DateTime.Now.ToString("HHmmss"); // + LoggerBehavior.sceneName;

    #endregion

    #region Properties


    public string FullPathLogDir
    {
        get { return AppConstants.DefaultEyeTrackingFolder; }
    }

    public string FullPathLogFile
    {
        get { return AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName + "\\" + FileName + ".csv"; }
    }

    public static Logger Instance
    {
        get
        {
            if (_instance == null) Create();
            return _instance;
        }
    }

    #endregion

    #region Constructor(s)

    public Logger()
    {
        InitDefaultFolder();
        InitSubjectFolder();
        InitSubjectLogFile();
    }

    #endregion

    #region Private Methods

    private static void Create()
    {
        _instance = new Logger();
    }

    #endregion

    #region Public Methods

    public void Log(object[] data)
    {
        using (var writer = new CsvWriter(FullPathLogFile))
        {
            foreach (var o in data)
            {
                writer.Write(o);
            }
        }
    }

    #endregion

    #region Init Methods

    private void InitDefaultFolder()
    {
        if (!Directory.Exists(AppConstants.DefaultEyeTrackingFolder))
            Directory.CreateDirectory(AppConstants.DefaultEyeTrackingFolder);
    }

    private void InitSubjectFolder()
    {
        if (!Directory.Exists(AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName))
            Directory.CreateDirectory(AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName);
    }

    private void InitSubjectLogFile()
    {
        if (!File.Exists(FullPathLogFile))
            File.Create(FullPathLogFile).Dispose();

    }
    #endregion
}
