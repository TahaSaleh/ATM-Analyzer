using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
//using System.Timers;
using System.Web.Http;
using ATM.Models;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class SimulationController : ApiController
    {
        
        private static ATMEntities db ;
        public static Timer timer_1, timer_2, timer_3,timer_4,timer;
        public static int atmid = 1;
        static int oldcount = 0;
        static Random rnd = new Random();
        //public static void update_atm()
        //{
        //   timer = new Timer( run_timer, null,300000,5000);


        //}
        //public static void  run_timer(object args)
        //{
            
        //    DateTime d1, d2;
        //    d1 = DateTime.Now;
        //    d2 = d1.AddHours(-1);
        //     for (int i=1 ;i<=20; i++)
        //    {
        //      trunsaction_Between_date(i, d2, d1);
              
        //    }

        //}

        public static void stop_timer_1()//to Stop timer 
        {
            timer_1.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public static void insertTrunsaction( DateTime starttime, DateTime endtime,decimal withdrow)
        {
            db = new ATMEntities();
            int id = rnd.Next(1, 21);
            var atm = db.ATMs.Where(x => x.id == id).FirstOrDefault();

            if (atm.balance > withdrow)
            {
                atm.balance -= withdrow;
                Models.Trunsaction trunsaction = new Models.Trunsaction()
                {
                    atm_id = id,
                    start_time = starttime,
                    end_time = endtime,
                    withdrow = withdrow
                };
                try
                {
                    db.Trunsactions.Add(trunsaction);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    db = null;
                    
                }
                
                //count_trunsaction_day(id);
                DateTime date1 = DateTime.Now;
                DateTime date2 = date1.AddHours(-1);
                trunsaction_Between_date(id, date2, date1);
            }
            
            db = null;
        }

        #region timer of insert trunsaction
        public static void start_random_timer()
        {
            int x = rnd.Next(1, 3)*30*1000;   // timer 1 or 2
            start_timer_insert_trunsaction(x);

        }
        public static  void start_timer_insert_trunsaction(int x)
        {
            // should changed
            timer_1 = new Timer(run1, null, x, 5000);
        }
        public static void run1(object args)
        {
            decimal w = rnd.Next(100,10001);
            DateTime start_time = DateTime.Now;
            DateTime end_time = start_time.AddMinutes(2);
            insertTrunsaction( start_time, end_time,w);
            stop_timer_1();
            int x = rnd.Next(1, 3)*30*1000;
            start_timer_insert_trunsaction(x);
            
        }
        #endregion
        ////public static int count_trunsaction_day(int atmid)
        //{
        //    db = new ATMEntities();
        //    //List<Trunsaction> z = db.Trunsactions.Where(i => i.atm_id == atmid && i.start_time.Value.ToShortDateString()==DateTime.Today.ToShortDateString()).ToList();
        //    //int y = z.Count;
        //    List<Trunsaction> t = db.Trunsactions.Where(i=>i.atm_id==atmid).ToList();
        //    int x = t.Count;
        //    var atm = db.ATMs.Where(i => i.id == atmid).FirstOrDefault();
        //    atm.T_N = x;
        //    //atm.T_N_last_day = y;
        //    db.SaveChanges();
        //    oldcount = x - oldcount;
        //    db = null;
        //    return oldcount;
        //}
        
        public static void trunsaction_Between_date(int atmid,DateTime starttime, DateTime endtime)
        {
            db = new ATMEntities();
            List<Trunsaction> q= db.Trunsactions.Where(i => i.atm_id==atmid).ToList();
            int y = q.Count;
            List<Trunsaction> t = q.Where(i => i.start_time >= starttime && i.start_time < endtime).ToList();
            int x = t.Count;
            var atm = db.ATMs.Where(i => i.id == atmid).FirstOrDefault();
            atm.T_N = y;
            atm.T_N_last_hour = x;
            db.SaveChanges();
            db = null;
            //return 1;
        }
        #region timer of trunsaction Between date
        public static void start_timer_trunsaction_Between_date()
        {
            timer_3 = new Timer(run3,null, 3600000, 5000);
        }
        public static void run3(object args)
        {
            // int count = trunsaction_Between_dates();
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddHours(-1);
            trunsaction_Between_date(atmid,date2,date1);
        }
        #endregion
        public static void insert_into_atm_amount(int atmid,int amount)
        {
            db = new ATMEntities();
            var c = db.ATMs.Where(i =>i.id== atmid).FirstOrDefault();
            c.balance += amount;
            db.SaveChanges();
            db = null;
        }

        #region insert into atm amount
        public static void start_timer_insert_into_atm()
        {
            timer_4 = new Timer(run4, null, 86400000, 5000);//every 24 hours ==>86400000
        }
        public static void run4(object args)
        {
            //int x= db.ATMs.ToList().Count;
            for (int i = 1; i <= 20; i++)
            {
                insert_into_atm_amount(i, 100000);
            }
        }
        #endregion
        int a=0;
        [HttpGet]
        //...../api/simulation
        public IHttpActionResult simulation()
        {
            if (a == 1)
            {
                timer_1.Change(Timeout.Infinite, Timeout.Infinite);
                //timer_4.Change(Timeout.Infinite, Timeout.Infinite);
            }
            //start_timer_insert_into_atm();
            start_random_timer();
            a = 1;
            return Ok();
        }
        [HttpGet]
        [Route("api/simulation/stop")]
        public IHttpActionResult stop()
        {
           
                timer_1.Change(Timeout.Infinite, Timeout.Infinite);
                //timer_4.Change(Timeout.Infinite, Timeout.Infinite);
          
            return Ok();
        }

        
    }

}