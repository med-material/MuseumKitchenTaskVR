using System;

public static class AppConstants
{

    #region General Constants

    public static string DefaultEyeTrackingFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\_GazeData";
    #endregion

    // not used
    #region OSCSystem constants
    public const string CalibrationCubeTag = "CCube";
    public const float CubeScaleRate = 0.75f;
    public const float CubeDistance = 2.0f;
    public const float CubeDepth = 0.1f;
    public const float TimeOutBeforeSizeUp = 1000f;
    public const int GridHeight = 3;
    public const int GridWidth = 3;
    #endregion

    #region LoggerBehavior Constants

    //public const string CsvFirstRow = "local_time;framerate;scene_events;timer;head_pos_x;head_pos_y;head_pos_z;head_ori_x;head_ori_y;head_ori_z;gaze_hemi_x;gaze_hemi_y;gaze_world_x;gaze_world_y;gazeConf;"; //head_ori_Ex;head_ori_Ey;head_ori_Ez;
    //public const string CsvFirstRowBaking = CsvFirstRow + "ctrlR_pos_x;ctrlR_pos_y;ctrlR_pos_z;ctrlR_rot_x;ctrlR_rot_y;ctrlR_rot_z;ctrl_event;last_bun_x;last_bun_y;last_bun_z;last_bun_name;";
    //public const string CsvFirstRowMusuem = CsvFirstRow + "imageset_id;imageset_order;obj_id;obj_pos_x;obj_pos_y;obj_pos_z;gaze_obj_x;gaze_obj_y;gaze_obj_z;";

    public const string CsvFirstRow = "personID;local_time;framerate;scene_events;timer;head_pos_x;head_pos_y;head_pos_z;head_ori_x;head_ori_y;head_ori_z;gaze_hemi_x;gaze_hemi_y;gaze_world_x;gaze_world_y;gazeConf;" +
    "ctrlR_pos_x;ctrlR_pos_y;ctrlR_pos_z;ctrlR_rot_x;ctrlR_rot_y;ctrlR_rot_z;ctrl_event;last_bun_name;last_bun_x;last_bun_y;last_bun_z;" +
    "imageset_id;imageset_order;obj_id;obj_pos_x;obj_pos_y;obj_pos_z;gaze_obj_x;gaze_obj_y;gaze_obj_z;";

    #endregion
}
