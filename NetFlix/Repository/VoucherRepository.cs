using NetFlix.EnityModel;
using NetFlix.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voucher = NetFlix.EnityModel.Voucher;

namespace NetFlix.Repository
{
    public class VoucherRepository
    {
        public Voucher CheckValidVoucher(string code)
        {
            using (var dbContext = new BookingMovieAppContext())
            {
                var voucher = dbContext.Vouchers.FirstOrDefault(v => v.VoucherCode.Equals(code));

                if (voucher != null && voucher.ValidFrom <= DateTime.Now && voucher.ValidUntil >= DateTime.Now && voucher.RemainingUsage > 0)
                {
                    return voucher;
                }
                return null;
            }
        }

        public ObservableCollection<Voucher> GetAllVoucher()
        {
            using(var dbContext = new BookingMovieAppContext())
            {
                try
                {
                    var vouchers = dbContext.Vouchers.ToList();
                    return new ObservableCollection<Voucher>(vouchers);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Occured error while getting all Voucher" + ex.Message); 
                    return null; 
                }
            }
        }
        public decimal? DiscountAmount(Voucher voucher, decimal original_price)
        {
            decimal? new_price = -1;
            using (var dbContext = new BookingMovieAppContext())
            {
                if (voucher.VoucherType.Equals("Percentage"))
                {
                    new_price = original_price * ((100 - voucher.DiscountValue) / 100);
                }
                else if (voucher.VoucherType.Equals("Fixed Amount"))
                {
                    new_price = original_price - voucher.DiscountValue;
                }
                else
                {
                    throw new Exception("VoucherType: InValid Format");
                }
            }
            return new_price < 0 ? 0 : new_price;
        }

        public void UpdateVoucher(Voucher newVoucher)
        {
            using(var context = new BookingMovieAppContext())
            {
                var voucher = context.Vouchers.FirstOrDefault(v => v.VoucherId == newVoucher.VoucherId);
                if(voucher != null)
                {
                    voucher = newVoucher;
                    context.SaveChanges(); 
                }
                else
                {
                    throw new Exception("Voucher cannot be found"); 
                }
            }
        }
        public bool AddVoucher(Voucher voucher)
        {
            using (var context = new BookingMovieAppContext())
            {
                context.Vouchers.Add(voucher);
                context.SaveChanges();
                return true;
            }
        }

        public void DeletetVoucher(Voucher voucher)
        {
            using (var context = new BookingMovieAppContext())
            {
                context.Vouchers.Remove(voucher); 
                context.SaveChanges();
            }
        }
    }

}
