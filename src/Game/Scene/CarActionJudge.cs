using Car;
using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class CarActionJudge : MonoBehaviour
	{
		private float turnLightTime;

		private int farAndNearAlternateIndex = -1;

		private const float ErrorValue = 0.07f;

		private int preViewPointState;

		private float AddSpeed3s;

		private int step;

		public static CarActionJudge Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		private void Update()
		{
			if (farAndNearAlternateIndex != -1)
			{
				JudgeFarAndNearLight(farAndNearAlternateIndex);
			}
			if (ViewJumpCtl.Instance.modelType != 2 && ViewJumpCtl.Instance.modelType != 1)
			{
				return;
			}
			if (CarState.Instance.CarSignal.turnLight == 1)
			{
				if (turnLightTime > 0f)
				{
					turnLightTime = 0f;
				}
				turnLightTime -= Time.deltaTime;
			}
			else if (CarState.Instance.CarSignal.turnLight == 2)
			{
				if (turnLightTime < 0f)
				{
					turnLightTime = 0f;
				}
				turnLightTime += Time.deltaTime;
			}
			else if (CarState.Instance.CarSignal.turnLight == 0)
			{
				turnLightTime = 0f;
			}
		}

		public void JudgeTurnLight3s(int action, int judgeType = 2)
		{
			if (ViewJumpCtl.Instance.trainType == TrainType.BasicTrain)
			{
				return;
			}
			switch (action)
			{
			case 109:
				if (turnLightTime > 0f)
				{
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType == 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未正确开启转向灯", 100f);
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未正确开启转向灯", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,未正确开启转向灯", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,未正确开启转向灯", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,未正确开启转向灯", 10f);
						break;
					}
				}
				else if (turnLightTime < 0f && turnLightTime > -3f)
				{
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,转向灯开启时间少于3秒", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,转向灯开启时间少于3秒", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,转向灯开启时间少于3秒", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,转向灯开启时间少于3秒", 10f);
						break;
					}
				}
				else
				{
					if (turnLightTime != 0f)
					{
						break;
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType == 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未开启转向灯", 100f);
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未开启转向灯", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,未开启转向灯", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,未开启转向灯", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,未开启转向灯", 10f);
						break;
					}
				}
				break;
			case 110:
				if (turnLightTime < 0f)
				{
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType == 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未正确开启转向灯", 100f);
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未正确开启转向灯", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,未正确开启转向灯", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,未正确开启转向灯", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,未正确开启转向灯", 10f);
						break;
					}
				}
				else if (turnLightTime > 0f && turnLightTime < 3f)
				{
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,转向灯开启时间少于3秒", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,转向灯开启时间少于3秒", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,转向灯开启时间少于3秒", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,转向灯开启时间少于3秒", 10f);
						break;
					}
				}
				else
				{
					if (turnLightTime != 0f)
					{
						break;
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType == 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未开启转向灯", 100f);
					}
					if (judgeType == 2 && ViewJumpCtl.Instance.itemType != 16)
					{
						SoundPlayCtl.Instance.PlaySound("变更车道前,未开启转向灯", 10f);
						break;
					}
					switch (judgeType)
					{
					case 1:
						SoundPlayCtl.Instance.PlaySound("转向前,未开启转向灯", 10f);
						break;
					case 3:
						SoundPlayCtl.Instance.PlaySound("掉头前,未开启转向灯", 10f);
						break;
					case 4:
						SoundPlayCtl.Instance.PlaySound("起步前,未开启转向灯", 10f);
						break;
					}
				}
				break;
			}
		}

		public string GetActionText(int action)
		{
			string result = "";
			switch (action)
			{
			case 0:
				result = "开启远光灯";
				break;
			case 1:
				result = "关闭远光灯";
				break;
			case 2:
				result = "开启近光灯";
				break;
			case 3:
				result = "关闭近光灯";
				break;
			case 113:
				result = "开启示宽灯";
				break;
			case 4:
				result = "开启示宽灯";
				break;
			case 5:
				result = "关闭示宽灯";
				break;
			case 6:
				result = "远近光灯交替一次";
				break;
			case 7:
				result = "远近光灯交替两次";
				break;
			case 8:
				result = "关闭所有灯光";
				break;
			case 9:
				result = "开启近光灯或远光灯";
				break;
			case 119:
				result = "操作正确，请关闭近光灯或远光灯";
				break;
			case 11:
				result = "开启右转向灯";
				break;
			case 12:
				result = "关闭转向灯";
				break;
			case 10:
				result = "开启左转向灯";
				break;
			case 50:
				result = "开启雾灯";
				break;
			case 51:
				result = "关闭雾灯";
				break;
			case 52:
				result = "开启双闪灯";
				break;
			case 53:
				result = "关闭双闪灯";
				break;
			case 59:
				result = "点火";
				break;
			case 57:
				result = "鸣喇叭";
				break;
			case 58:
				result = "停止鸣喇叭";
				break;
			case 60:
				result = "打开电源";
				break;
			case 61:
				result = "关闭电源";
				break;
			case 13:
				result = "方向盘左打满";
				break;
			case 16:
				result = "方向盘左转一圈";
				break;
			case 15:
				result = "回正方向盘";
				break;
			case 17:
				result = "方向盘右转一圈";
				break;
			case 14:
				result = "方向盘右打满";
				break;
			case 44:
				result = "左转方向盘";
				break;
			case 45:
				result = "右转方向盘";
				break;
			case 32:
				result = "方向盘左转90度";
				break;
			case 33:
				result = "方向盘右转90度";
				break;
			case 30:
				result = "方向盘左转半圈";
				break;
			case 31:
				result = "方向盘右转半圈";
				break;
			case 200:
				result = "方向盘右打满的位置回半圈";
				break;
			case 201:
				result = "方向盘左一圈加90度";
				break;
			case 202:
				result = "方向盘左打满的位置回半圈";
				break;
			case 203:
				result = "方向盘右一圈加90度";
				break;
			case 18:
				result = "离合器踩到底";
				break;
			case 19:
				result = "轻踩离合器";
				break;
			case 20:
				result = "松离合器";
				break;
			case 41:
				result = "缓慢松离合器";
				break;
			case 21:
				result = "脚刹踩到底";
				break;
			case 22:
				result = "轻踩脚刹";
				break;
			case 23:
				result = "松脚刹";
				break;
			case 24:
				result = "油门踩到底";
				break;
			case 25:
				result = "轻踩油门";
				break;
			case 26:
				result = "松油门";
				break;
			case 27:
				result = "拉紧手刹";
				break;
			case 28:
				result = "轻拉手刹";
				break;
			case 29:
				result = "松手刹";
				break;
			case 34:
				result = "挂一挡";
				break;
			case 35:
				result = "挂二挡";
				break;
			case 36:
				result = "挂三挡";
				break;
			case 37:
				result = "挂四挡";
				break;
			case 38:
				result = "挂五挡";
				break;
			case 39:
				result = "挂空挡";
				break;
			case 40:
				result = "挂倒挡";
				break;
			case 46:
				result = "系上安全带";
				break;
			case 47:
				result = "松开安全带";
				break;
			case 49:
				result = "打开车门";
				break;
			case 48:
				result = "关闭车门";
				break;
			case 55:
				result = "开启雨刮器快刮";
				break;
			case 54:
				result = "开启雨刮器慢刮";
				break;
			case 204:
				result = "开启雨刮器间歇刮";
				break;
			case 56:
				result = "关闭雨刮器";
				break;
			case 101:
				result = "进行起步操作";
				break;
			case 102:
				result = "进行起步操作";
				break;
			case 103:
				result = "进行停车操作";
				break;
			case 104:
				result = "进行停车操作";
				break;
			case 105:
				result = "车辆向前行驶";
				break;
			case 106:
				result = "车辆向后行驶";
				break;
			case 107:
				result = "保持车速在30km/h以下";
				break;
			case 108:
				result = "保持车速在30km/h以上";
				break;
			case 1080:
				result = "保持车速在40km/h以上3秒";
				break;
			case 42:
				result = "熄火";
				break;
			case 43:
				result = "点火";
				break;
			case 109:
				result = "开左转向灯大于3秒";
				break;
			case 110:
				result = "开右转向灯大于3秒";
				break;
			case 111:
				result = "提高车速";
				break;
			case 112:
				result = "降低车速";
				break;
			case 65:
				result = "挂D挡";
				break;
			case 63:
				result = "挂P挡";
				break;
			case 64:
				result = "挂L挡";
				break;
			case 66:
				result = "挂R挡";
				break;
			default:
				Debug.Log("没有对应操作的文本");
				break;
			}
			return result;
		}

		public bool JudgeActionDone(int action)
		{
			bool result = false;
			if (JudgeLightActionDone(action))
			{
				result = true;
			}
			else if (JudgePedalActionDone(action))
			{
				result = true;
			}
			else if (JudgeSampleOtherActionDone(action))
			{
				result = true;
			}
			else if (JudgeComplexActionDone(action))
			{
				result = true;
			}
			return result;
		}

		public bool JudgeActionDone(string actionContent)
		{
			int actionId = GetActionId(actionContent);
			if (actionId != -1)
			{
				return JudgeActionDone(actionId);
			}
			return false;
		}

		private bool JudgeLightActionDone(int action)
		{
			bool result = false;
			switch (action)
			{
			case -1:
				result = true;
				break;
			case 0:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 2)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.frontLight == 2 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 1:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 2)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.frontLight != 2 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 2:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.frontLight == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 3:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight != 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.frontLight != 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 4:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.widthLight == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 5:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.widthLight == 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 6:
				farAndNearAlternateIndex = 6;
				break;
			case 7:
				farAndNearAlternateIndex = 7;
				break;
			case 8:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.widthLight == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.frontLight == 0 && CarState.Instance.CarSignal.widthLight == 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 9:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 2 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 1))
				{
					result = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1) && (CarState.Instance.CarSignal.frontLight == 2 || CarState.Instance.CarSignal.frontLight == 1))
				{
					result = true;
				}
				break;
			case 119:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					result = true;
				}
				else if ((ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1) && CarState.Instance.CarSignal.frontLight == 0)
				{
					result = true;
				}
				break;
			case 10:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.turnLight == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 11:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 2)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.turnLight == 2 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 12:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.turnLight == 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 52:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight == 3)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.turnLight == 3 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 53:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.turnLight != 3)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.turnLight != 3 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 50:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fogLight == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.fogLight == 1 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			case 51:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fogLight == 0 || !SettingInfoCtl.Instance.isJudgeFogLight))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.fogLight == 0 || (!SettingInfoCtl.Instance.isJudgeFogLight && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1)))
				{
					result = true;
				}
				break;
			case 113:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.widthLight == 1 && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.frontLight == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.widthLight == 1 && CarState.Instance.CarSignal.frontLight == 0 && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			}
			return result;
		}

		private bool JudgePedalActionDone(int action)
		{
			bool result = false;
			switch (action)
			{
			case 200:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 360f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 201:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 450f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 202:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 360f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 203:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 450f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 13:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 1f) < 0.14f)
				{
					result = true;
				}
				break;
			case 16:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 360f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 30:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 180f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 32:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel + 90f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 15:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel) < 0.07f)
				{
					result = true;
				}
				break;
			case 14:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 1f) < 0.14f)
				{
					result = true;
				}
				break;
			case 17:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 360f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 31:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 180f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 33:
				if (Mathf.Abs(CarState.Instance.CarSignal.steerWheel - 90f / CarSignalValue.SteerWheeMaxlAngle) < 0.07f)
				{
					result = true;
				}
				break;
			case 44:
				if (CarState.Instance.CarSignal.steerWheel < -0.08f)
				{
					result = true;
				}
				break;
			case 45:
				if (CarState.Instance.CarSignal.steerWheel > 0.08f)
				{
					result = true;
				}
				break;
			case 18:
				if (CarState.Instance.CarSignal.cluth > 0.75f)
				{
					result = true;
				}
				break;
			case 19:
				if (Mathf.Abs(CarState.Instance.CarSignal.cluth - 0.5f) < 0.25f)
				{
					result = true;
				}
				break;
			case 20:
				if (CarState.Instance.CarSignal.cluth < 0.15f)
				{
					result = true;
				}
				break;
			case 41:
				if ((ViewJumpCtl.Instance.trainType == TrainType.CourseTwo || ViewJumpCtl.Instance.trainType == TrainType.NanChangKaoChang) && CarState.Instance.CarSignal.cluth < 0.85f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.cluth < 0.2f)
				{
					result = true;
				}
				break;
			case 24:
				if (CarState.Instance.CarSignal.oil > 0.8f)
				{
					result = true;
				}
				break;
			case 25:
				if (CarState.Instance.CarSignal.oil > 0.08f && CarState.Instance.CarSignal.oil < 0.6f)
				{
					result = true;
				}
				break;
			case 26:
				if (CarState.Instance.CarSignal.oil < 0.1f)
				{
					result = true;
				}
				break;
			case 21:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.75f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.footBrake > 0.75f)
				{
					result = true;
				}
				break;
			case 22:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.1f && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.6f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.footBrake > 0.1f && CarState.Instance.CarSignal.footBrake < 0.6f)
				{
					result = true;
				}
				break;
			case 69:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake > 0.15f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.footBrake > 0.15f)
				{
					result = true;
				}
				break;
			case 23:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.1f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.footBrake < 0.1f)
				{
					result = true;
				}
				break;
			case 27:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake >= 0.15f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.handBrake >= 0.15f)
				{
					result = true;
				}
				break;
			case 28:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake > 0.1f && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake < 0.6f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.handBrake > 0.1f && CarState.Instance.CarSignal.handBrake < 0.6f)
				{
					result = true;
				}
				break;
			case 29:
				if (CarState.Instance.CarSignal.handBrake < 0.15f)
				{
					result = true;
				}
				break;
			case 70:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.handBrake > 0.15f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.handBrake > 0.15f && (ViewJumpCtl.Instance.modelType == 2 || ViewJumpCtl.Instance.modelType == 1))
				{
					result = true;
				}
				break;
			}
			return result;
		}

		public int GetActionId(string actionText)
		{
			int num = -1;
			switch (actionText)
			{
			case "开启远光灯":
				return 0;
			case "关闭远光灯":
				return 1;
			case "开启近光灯":
				return 2;
			case "关闭近光灯":
				return 3;
			case "开启示宽灯":
				return 4;
			case "关闭示宽灯":
				return 5;
			case "远近光灯交替一次":
				return 6;
			case "远近光灯交替两次":
				return 7;
			case "关闭所有灯光":
				return 8;
			case "开启近光灯或远光灯":
				return 9;
			case "开启右转向灯":
				return 11;
			case "关闭转向灯":
				return 12;
			case "开启左转向灯":
				return 10;
			case "打开左转向灯":
				return 10;
			case "开启雾灯":
				return 50;
			case "关闭雾灯":
				return 51;
			case "开启双闪灯":
				return 52;
			case "关闭双闪灯":
				return 53;
			case "鸣喇叭":
				return 57;
			case "停止鸣喇叭":
				return 58;
			case "打开电源":
				return 60;
			case "关闭电源":
				return 61;
			case "方向盘左打满":
				return 13;
			case "方向盘左转一圈":
				return 16;
			case "回正方向盘":
				return 15;
			case "方向盘右转一圈":
				return 17;
			case "回正方向,同时在向右打一圈":
				return 17;
			case "方向盘右打满":
				return 14;
			case "左转方向盘":
				return 44;
			case "右转方向盘":
				return 45;
			case "方向盘左转90度":
				return 32;
			case "方向盘右转90度":
				return 33;
			case "方向盘右转半圈":
				return 31;
			case "方向盘左转半圈":
				return 30;
			case "方向盘右打满的位置回半圈":
				return 200;
			case "方向盘左一圈加90度":
				return 201;
			case "方向盘左打满的位置回半圈":
				return 202;
			case "方向盘右一圈加90度":
				return 203;
			case "离合器踩到底":
				return 18;
			case "轻踩离合器":
				return 41;
			case "松离合器":
				return 20;
			case "缓慢松离合器":
				return 41;
			case "缓慢松离合器至车身抖动":
			case "缓慢松离合器至一半":
				return 19;
			case "脚刹缓慢踩到底":
			case "脚刹踩到底":
				return 21;
			case "轻踩脚刹":
				return 22;
			case "松脚刹":
				return 23;
			case "油门踩到底":
				return 24;
			case "轻踩油门":
				return 25;
			case "松油门":
				return 26;
			case "拉紧手刹":
				return 27;
			case "轻拉手刹":
				return 28;
			case "松手刹":
				return 29;
			case "进行起步操作":
				return 101;
			case "进行停车操作":
				return 103;
			case "间歇刮雨刮器":
				return 204;
			case "慢刮雨刮器":
				return 54;
			case "快刮雨刮器":
				return 55;
			case "挂一挡":
				return 34;
			case "挂二挡":
				return 35;
			case "挂三挡":
				return 36;
			case "逐步将挡位加挡至三挡":
				return 36;
			case "挂四挡":
				return 37;
			case "挂五挡":
				return 38;
			case "挂空挡":
				return 39;
			case "挂倒挡":
				return 40;
			case "熄火":
				return 42;
			case "点火":
				return 43;
			case "挂N挡":
				return 67;
			case "挂P挡":
				return 63;
			case "挂L挡":
				return 64;
			case "挂R挡":
				return 66;
			case "挂D挡":
				return 65;
			case "缓慢松脚刹":
				return 68;
			case "车辆向前行驶":
				return 105;
			case "车辆向后行驶":
				return 106;
			case "保持车速在30km/h以下":
				return 107;
			case "保持车速在30km/h以上":
				return 108;
			case "保持车速在40km/h以上3秒":
				return 1080;
			case "开左转向灯大于3秒":
				return 109;
			case "开右转向灯大于3秒":
				return 110;
			case "降低车速":
				return 112;
			case "提高车速":
				return 111;
			case "...":
				return -2;
			default:
				Debug.Log("没有对应文本的操作 : " + actionText);
				return -1;
			}
		}

		private bool JudgeSampleOtherActionDone(int action)
		{
			bool result = false;
			switch (action)
			{
			case 34:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 1)
				{
					result = true;
				}
				break;
			case 35:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 2)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 2)
				{
					result = true;
				}
				break;
			case 36:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 3)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 3)
				{
					result = true;
				}
				break;
			case 37:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 4)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 4)
				{
					result = true;
				}
				break;
			case 38:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 5)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 5)
				{
					result = true;
				}
				break;
			case 39:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 0)
				{
					result = true;
				}
				break;
			case 40:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 6)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 6)
				{
					result = true;
				}
				break;
			case 49:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && SettingInfoCtl.Instance.isJudgeDoor && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.door == 1)
				{
					result = true;
				}
				else if (SettingInfoCtl.Instance.isJudgeDoor && CarState.Instance.CarSignal.door == 1)
				{
					result = true;
				}
				break;
			case 48:
				if (!SettingInfoCtl.Instance.isJudgeDoor || CarState.Instance.CarSignal.door == 0)
				{
					result = true;
				}
				break;
			case 47:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && SettingInfoCtl.Instance.isJudgeSafetyBelt && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.safeBelt == 1)
				{
					result = true;
				}
				else if (SettingInfoCtl.Instance.isJudgeSafetyBelt && CarState.Instance.CarSignal.safeBelt == 1)
				{
					result = true;
				}
				break;
			case 46:
				if (!SettingInfoCtl.Instance.isJudgeSafetyBelt || CarState.Instance.CarSignal.safeBelt == 0)
				{
					result = true;
				}
				break;
			case 55:
				if (CarState.Instance.CarSignal.rainBrush == 2)
				{
					result = true;
				}
				break;
			case 54:
				if (CarState.Instance.CarSignal.rainBrush == 1)
				{
					result = true;
				}
				break;
			case 204:
				if (CarState.Instance.CarSignal.rainBrush == 3)
				{
					result = true;
				}
				break;
			case 56:
				if (CarState.Instance.CarSignal.rainBrush == 0)
				{
					result = true;
				}
				break;
			case 59:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.fire == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.fire == 1)
				{
					result = true;
				}
				break;
			case 57:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.horn == 1)
				{
					result = true;
				}
				break;
			case 58:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.horn == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.horn == 0)
				{
					result = true;
				}
				break;
			case 60:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.power == 1)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.power == 1)
				{
					result = true;
				}
				break;
			case 61:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.power == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.power == 0)
				{
					result = true;
				}
				break;
			case 62:
				if (preViewPointState != CarState.Instance.CarSignal.ChangeViewPoint)
				{
					result = true;
					preViewPointState = CarState.Instance.CarSignal.ChangeViewPoint;
				}
				break;
			case 63:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 5)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 101)
				{
					result = true;
				}
				break;
			case 65:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 1 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 4))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 100)
				{
					result = true;
				}
				break;
			case 64:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 2 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 3))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 103)
				{
					result = true;
				}
				break;
			case 66:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 6 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 6))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 102)
				{
					result = true;
				}
				break;
			case 67:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 104)
				{
					result = true;
				}
				break;
			case 68:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.footBrake < 0.45f)
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.footBrake < 0.45f)
				{
					result = true;
				}
				break;
			}
			return result;
		}

		private bool JudgeComplexActionDone(int action)
		{
			bool result = false;
			switch (action)
			{
			case 101:
				if (CarState.Instance.FireOn)
				{
					result = true;
				}
				break;
			case 103:
				if (CarState.Instance.CurSpeed < 0.1f)
				{
					result = true;
				}
				break;
			case 105:
				if (CarState.Instance.CurSpeed > 0.2f && CarState.Instance.CarSignal.gear != 0 && CarState.Instance.CarSignal.gear != 6)
				{
					result = true;
				}
				break;
			case 106:
				if (CarState.Instance.CurSpeed > 0.2f && CarState.Instance.CarSignal.gear == 6)
				{
					result = true;
				}
				break;
			case 107:
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					if (CarDriveHistoryPlay.Instance.curCalculateSpeed < 30f)
					{
						result = true;
					}
				}
				else if (CarState.Instance.CurSpeed < 30f)
				{
					result = true;
				}
				break;
			case 108:
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					if (CarDriveHistoryPlay.Instance.curCalculateSpeed >= 30f)
					{
						result = true;
					}
				}
				else if (CarState.Instance.CurSpeed >= 30f)
				{
					result = true;
				}
				break;
			case 1080:
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					if (CarDriveHistoryPlay.Instance.curCalculateSpeed >= 40f)
					{
						AddSpeed3s += Time.deltaTime;
					}
					else
					{
						AddSpeed3s = 0f;
					}
				}
				else if (CarState.Instance.CurSpeed >= 40f)
				{
					AddSpeed3s += Time.deltaTime;
				}
				else
				{
					AddSpeed3s = 0f;
				}
				if (ViewJumpCtl.Instance.modelType == 1)
				{
					if (CarDriveHistoryPlay.Instance.curCalculateSpeed >= 40f && AddSpeed3s > 3f)
					{
						result = true;
					}
				}
				else if (CarState.Instance.CurSpeed >= 40f && AddSpeed3s > 3f)
				{
					result = true;
				}
				break;
			case 42:
				if (!CarState.Instance.FireOn)
				{
					result = true;
				}
				break;
			case 43:
				if (CarState.Instance.FireOn)
				{
					result = true;
				}
				break;
			case 109:
				if (turnLightTime < -3f)
				{
					result = true;
				}
				break;
			case 110:
				if (turnLightTime > 3f)
				{
					result = true;
				}
				break;
			case 111:
				if (CarState.Instance.CurSpeed >= 3f)
				{
					result = true;
				}
				break;
			case 112:
				if (CarState.Instance.CurSpeed <= 3f)
				{
					result = true;
				}
				break;
			case 114:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 102 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 6))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 102 || CarState.Instance.CarSignal.gear == 6)
				{
					result = true;
				}
				break;
			case 115:
				if ((ViewJumpCtl.Instance.modelType == 0 || ViewJumpCtl.Instance.modelType == 5) && DriveHistoryPlay.Instance.curIndexPlay < DriveHistoryPlay.Instance.carDriveRecordList.Count && (DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 104 || DriveHistoryPlay.Instance.carDriveRecordList[DriveHistoryPlay.Instance.curIndexPlay].carSignal.gear == 0))
				{
					result = true;
				}
				else if (CarState.Instance.CarSignal.gear == 104 || CarState.Instance.CarSignal.gear == 0)
				{
					result = true;
				}
				break;
			case -2:
				result = true;
				break;
			}
			return result;
		}

		private void JudgeFarAndNearLight(int action)
		{
			if (step != 99 && action == 6)
			{
				if (ViewJumpCtl.Instance.itemType == 701)
				{
					if (step == 0 && JudgeActionDone(0))
					{
						step = 1;
					}
					else if (step == 1 && JudgeActionDone(2))
					{
						step = 2;
					}
					else if (step == 2 && JudgeActionDone(0))
					{
						step = 99;
					}
				}
				else if (step == 0 && JudgeActionDone(2))
				{
					step = 1;
				}
				else if (step == 1 && JudgeActionDone(0))
				{
					step = 2;
				}
				else if (step == 2 && JudgeActionDone(2))
				{
					step = 99;
				}
			}
			else if (step != 99 && action == 7)
			{
				if (step == 0 && JudgeActionDone(2))
				{
					step = 1;
				}
				else if (step == 1 && JudgeActionDone(0))
				{
					step = 2;
				}
				else if (step == 2 && JudgeActionDone(2))
				{
					step = 3;
				}
				else if (step == 3 && JudgeActionDone(0))
				{
					step = 4;
				}
				else if (step == 4 && JudgeActionDone(2))
				{
					step = 99;
				}
			}
		}

		public int GetFarAndNearActionDone()
		{
			int result = 0;
			if (step == 99)
			{
				result = 2;
			}
			else if (step > 2)
			{
				result = 1;
			}
			step = 0;
			farAndNearAlternateIndex = -1;
			return result;
		}
	}
}
