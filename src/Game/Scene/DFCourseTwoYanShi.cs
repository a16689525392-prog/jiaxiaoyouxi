using Global;
using SoundAndTextShow;
using UnityEngine;
using View;

namespace Scene
{
	public class DFCourseTwoYanShi : CourseTwoItemCtlBase
	{
		private bool isOverTest;

		private bool isDaoZhuang;

		private bool isCeFang;

		private bool isPoDao;

		private bool isQuXian;

		private bool isZhiJiao;

		private bool isDanBianQiao;

		private bool isQiFuLu;

		private bool isZhangAi;

		private bool isXianKuan;

		private bool isWuTian;

		private bool isYuTian;

		private bool isShiHua;

		private bool isMoNiSuiDao;

		private bool isMoNiZhuanWan;

		private bool isMoNiGaoSu;

		private bool isZhaiLu;

		protected override void Start()
		{
			base.Start();
			if (ViewJumpCtl.Instance.modelType == 6)
			{
				SoundPlayCtl.Instance.PlaySound("调整桌椅，系好安全带，本次考试开始");
			}
		}

		private void Update()
		{
			if (ViewJumpCtl.Instance.modelType == 6 && MoNiGaoSu.kaoShiJieShu)
			{
				if (DataCenter.Instance.score >= 80f && !isOverTest)
				{
					SoundPlayCtl.Instance.PlaySound("考试通过，请考生将车开回起点");
					isOverTest = true;
				}
				else if (DataCenter.Instance.score < 80f && !isOverTest)
				{
					SoundPlayCtl.Instance.PlaySound("本次考试不合格");
					isOverTest = true;
				}
			}
		}

		public override void TriggerEvent(Collider other)
		{
			base.TriggerEvent(other);
			if (ViewJumpCtl.Instance.modelType == 0)
			{
				if (other.name == "Start_CeFang" && !isCeFang)
				{
					SoundPlayCtl.Instance.PlaySound("侧方停车项目开始");
					isCeFang = true;
					itemStep = 1;
				}
				else if (other.name == "End_CeFang" && itemStep == 1)
				{
					SoundPlayCtl.Instance.PlaySound("侧方停车项目结束");
					itemStep = 2;
				}
				if (other.name == "Start_DaoZhuang" && !isDaoZhuang)
				{
					SoundPlayCtl.Instance.PlaySound("倒桩考试开始");
					isDaoZhuang = true;
					itemStep = 3;
				}
				else if (other.name == "End_DaoZhuang" && itemStep == 3)
				{
					SoundPlayCtl.Instance.PlaySound("倒桩考试结束");
					itemStep = 4;
				}
				if (other.name == "Start_PoDao" && !isPoDao)
				{
					SoundPlayCtl.Instance.PlaySound("坡道定点项目开始");
					isPoDao = true;
					itemStep = 5;
				}
				else if (other.name == "End_PoDao" && itemStep == 5)
				{
					SoundPlayCtl.Instance.PlaySound("坡道定点项目结束");
					itemStep = 6;
				}
				if (other.name == "Start_Qiao" && !isDanBianQiao)
				{
					SoundPlayCtl.Instance.PlaySound("通过单边桥考试开始");
					isDanBianQiao = true;
					itemStep = 7;
				}
				else if (other.name == "End_Qiao" && itemStep == 7)
				{
					SoundPlayCtl.Instance.PlaySound("通过单边桥考试结束");
					itemStep = 8;
				}
				if (other.name == "Start_QiFu" && !isQiFuLu)
				{
					SoundPlayCtl.Instance.PlaySound("起伏路驾驶考试开始");
					isQiFuLu = true;
					itemStep = 9;
				}
				else if (other.name == "End_QiFu" && itemStep == 9)
				{
					SoundPlayCtl.Instance.PlaySound("起伏路驾驶考试结束");
					itemStep = 10;
				}
				if (other.name == "Start_ZhiJiao" && !isZhiJiao)
				{
					SoundPlayCtl.Instance.PlaySound("直角转弯项目开始");
					isZhiJiao = true;
					itemStep = 11;
				}
				else if (other.name == "End_ZhiJiao" && itemStep == 11)
				{
					SoundPlayCtl.Instance.PlaySound("直角转弯项目结束");
					itemStep = 12;
				}
				if (other.name == "Start_ZhangAi" && !isZhangAi)
				{
					SoundPlayCtl.Instance.PlaySound("通过连续障碍考试开始");
					isZhangAi = true;
					itemStep = 13;
				}
				else if (other.name == "End_ZhangAi" && itemStep == 13)
				{
					SoundPlayCtl.Instance.PlaySound("通过连续障碍考试结束");
					itemStep = 14;
				}
				if (other.name == "Start_QuXian" && !isQuXian)
				{
					SoundPlayCtl.Instance.PlaySound("曲线行驶项目开始");
					isQuXian = true;
					itemStep = 15;
				}
				else if (other.name == "End_QuXian" && itemStep == 15)
				{
					SoundPlayCtl.Instance.PlaySound("曲线行驶项目结束");
					itemStep = 16;
				}
				if (other.name == "Start_XianKuan" && !isXianKuan)
				{
					SoundPlayCtl.Instance.PlaySound("限速通过限宽门考试开始");
					isXianKuan = true;
					itemStep = 17;
				}
				else if (other.name == "End_XianKuan" && itemStep == 17)
				{
					SoundPlayCtl.Instance.PlaySound("限速通过限宽门考试结束");
					itemStep = 18;
				}
				if (other.name == "Start_WuTian" && !isWuTian)
				{
					SoundPlayCtl.Instance.PlaySound("模拟雾天行驶考试开始");
					isWuTian = true;
					itemStep = 19;
				}
				else if (other.name == "End_WuTian" && itemStep == 19)
				{
					SoundPlayCtl.Instance.PlaySound("模拟雾天行驶考试结束");
					itemStep = 20;
				}
				if (other.name == "Start_YuTian" && !isYuTian)
				{
					SoundPlayCtl.Instance.PlaySound("模拟雨天行驶考试开始");
					isYuTian = true;
					itemStep = 21;
				}
				else if (other.name == "End_YuTian" && itemStep == 21)
				{
					SoundPlayCtl.Instance.PlaySound("模拟雨天行驶考试结束");
					itemStep = 22;
				}
				if (other.name == "Start_ShiHua" && !isShiHua)
				{
					SoundPlayCtl.Instance.PlaySound("湿滑路面考试开始");
					isShiHua = true;
					itemStep = 23;
				}
				else if (other.name == "End_ShiHua" && itemStep == 23)
				{
					SoundPlayCtl.Instance.PlaySound("湿滑路面考试结束");
					itemStep = 24;
				}
				if (other.name == "Start_SuiDao" && !isMoNiSuiDao)
				{
					SoundPlayCtl.Instance.PlaySound("模拟隧道考试开始");
					isMoNiSuiDao = true;
					itemStep = 25;
				}
				else if (other.name == "End_SuiDao" && itemStep == 25)
				{
					SoundPlayCtl.Instance.PlaySound("模拟隧道考试结束");
					itemStep = 26;
				}
				if (other.name == "Start_ZhuanWan" && !isMoNiZhuanWan)
				{
					SoundPlayCtl.Instance.PlaySound("模拟连续急转弯山区路行驶考试开始");
					isMoNiZhuanWan = true;
					itemStep = 27;
				}
				else if (other.name == "End_ZhuanWan" && itemStep == 27)
				{
					SoundPlayCtl.Instance.PlaySound("模拟连续急转弯山区路行驶考试结束");
					itemStep = 28;
				}
				if (other.name == "Start_GaoSu" && !isMoNiGaoSu)
				{
					SoundPlayCtl.Instance.PlaySound("模拟高速公路考试开始");
					isMoNiGaoSu = true;
					itemStep = 29;
				}
				else if (other.name == "End_GaoSu" && itemStep == 29)
				{
					SoundPlayCtl.Instance.PlaySound("模拟高速公路考试结束");
					itemStep = 30;
				}
				if (other.name == "Start_ZhaiLu" && !isZhaiLu)
				{
					SoundPlayCtl.Instance.PlaySound("窄路掉头考试开始");
					isZhaiLu = true;
					itemStep = 31;
				}
				else if (other.name == "Step1" && itemStep == 31)
				{
					itemStep = 32;
				}
				else if (other.name == "End_ZhaiLu" && itemStep == 32)
				{
					SoundPlayCtl.Instance.PlaySound("窄路掉头考试结束");
					itemStep = 33;
				}
			}
		}
	}
}
