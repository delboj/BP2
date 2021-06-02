using HotelGUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelGUI.ViewModel
{
    class HomeViewModel : BindableBase
    {
        private string tableListItem;
        public object table;
        public object tuple;
        private string validationError;

        public ObservableCollection<string> TableList { get; set; }

        public MyICommand<string> NavCommand { get; private set; }

        public HomeViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            TableList = new ObservableCollection<string> { "Drzi", "Gost", "Hotel", "Konobar", "Menadzer", "Odseda", "Radnik", "Recepcionar", "Restoran", "Soba", "Spremacica", "Vlasnik" };
            TableListItem = "";
        }

        public object Table
        {
            get { return table; }
            set { SetProperty(ref table, value); }
        }

        public object Tuple
        {
            get { return tuple; }
            set { SetProperty(ref tuple, value); }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "dodaj":
                    addTuple();
                    break;
                case "modifikuj":
                    modifyTuple();
                    break;
                case "obrisi":
                    deleteTuple();
                    break;
                case "procedura":
                    using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
                    {
                        var query = db.Database.SqlQuery<Decimal>("EXEC dbo.Procedura '1';");
                        ObservableCollection<Audit> ret = new ObservableCollection<Audit>(db.Audit.ToList());
                        string ispis = "Drugi gost u hotelima vlasnika: ";
                        foreach(Audit a in ret)
                        {
                            ispis += "\n\t" + a.Jmbg.ToString() + "\t" + a.Ime + "\t" + a.Prezime;
                        }
                        MessageBox.Show(ispis);
                    }
                    break;
                case "funkcija":
                    using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
                    {
                        Decimal query = db.Database.SqlQuery<Decimal>("SELECT dbo.Function1('Fontana');").First();
                        MessageBox.Show("Soba u kojoj je odselo najvise gostiju je: " + query);
                    }
                    break;
            }
        }

        public void addTuple()
        {
            using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
            {
                if (Tuple == null)
                {
                    ValidationError = "Morate izabrati neki red iz tabele.";
                }
                else if (Tuple.ToString() == "{NewItemPlaceholder}")
                {
                    ValidationError = "Morate nesto upisati.";
                }
                else
                {
                    switch (tableListItem)
                    {
                        case "Drzi":
                            try
                            {
                                db.drzi.Add((drzi)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Id hotela i maticni broj vlasnika moraju da postoje.";
                            }

                            break;
                        case "Gost":
                            try
                            {
                                db.gost.Add((gost)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan jmbg koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Hotel":
                            try
                            {
                                db.hotel.Add((hotel)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan id koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Konobar":
                            try
                            {
                                List<radnik> results = (from p in db.radnik
                                                        where p.jmbgradnika == ((konobar)Tuple).jmbgradnika
                                                        select p).ToList();

                                if (results.Count == 0)
                                {
                                    ValidationError = "Morate uneti validan jmbg koji postoji.";
                                    Table = new ObservableCollection<konobar>(db.konobar.ToList());
                                }
                                else
                                {
                                    try
                                    {
                                        db.konobar.Add((konobar)Tuple);
                                    }
                                    catch (InvalidOperationException)
                                    {

                                    }
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                            }
                            catch (DbUpdateException e)
                            {
                                ValidationError = e.InnerException.InnerException.Message;
                                Table = new ObservableCollection<konobar>(db.konobar.ToList());
                            }

                            break;
                        case "Menadzer":
                            try
                            {
                                List<radnik> results = (from p in db.radnik
                                                        where p.jmbgradnika == ((menadzer)Tuple).jmbgradnika
                                                        select p).ToList();

                                if (results.Count == 0)
                                {
                                    ValidationError = "Morate uneti validan jmbg koji postoji.";
                                }
                                else
                                {
                                    db.menadzer.Add((menadzer)Tuple);
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan jmbg koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Odseda":
                            try
                            {
                                db.odseda.Add((odseda)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Jmbg gosta, id hotela i broj sobe u tom hotelu moraju da postoje.";
                            }

                            break;
                        case "Radnik":
                            try
                            {
                                db.radnik.Add((radnik)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan jmbg koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Recepcionar":
                            try
                            {
                                List<radnik> results = (from p in db.radnik
                                                        where p.jmbgradnika == ((recepcionar)Tuple).jmbgradnika
                                                        select p).ToList();

                                if (results.Count == 0)
                                {
                                    ValidationError = "Morate uneti validan jmbg koji postoji.";
                                }
                                else
                                {
                                    db.recepcionar.Add((recepcionar)Tuple);
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan jmbg koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Restoran":
                            try
                            {
                                db.restoran.Add((restoran)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan id restorana koji ne postoji. Ukoliko se restoran nalazi u hotelu, id hotela mora biti validan.";
                            }

                            break;
                        case "Soba":
                            try
                            {
                                db.soba.Add((soba)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti sobu s validnim brojem koji ne postoji u zadatom hotelu.";
                            }

                            break;
                        case "Spremacica":
                            try
                            {
                                List<radnik> results = (from p in db.radnik
                                                        where p.jmbgradnika == ((spremacica)Tuple).jmbgradnika
                                                        select p).ToList();

                                if (results.Count == 0)
                                {
                                    ValidationError = "Morate uneti validan jmbg koji postoji.";
                                }
                                else
                                {
                                    db.spremacica.Add((spremacica)Tuple);
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan jmbg koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                        case "Vlasnik":
                            try
                            {
                                db.vlasnik.Add((vlasnik)Tuple);
                                db.SaveChanges();
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Morate uneti validan mbr koji ne postoji.";
                            }
                            catch (DbEntityValidationException)
                            {
                                ValidationError = "Sva polja moraju biti popunjena.";
                            }

                            break;
                    }
                }
            }
        }

        public void modifyTuple()
        {
            using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
            {
                if (Tuple == null || Tuple.ToString() == "{NewItemPlaceholder}")
                {
                    ValidationError = "Morate izabrati neki red iz tabele.";
                }
                else
                {
                    switch (tableListItem)
                    {
                        case "Drzi":
                            List<drzi> results1 = (from p in db.drzi
                                                   where p.hotel_idhotel == ((drzi)Tuple).hotel_idhotel && p.vlasnik_mbr == ((drzi)Tuple).vlasnik_mbr
                                                   select p).ToList();

                            if(results1.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci id hotela i mbr vlasnika.";
                            }
                            else
                            {
                                foreach (drzi d in results1)
                                {
                                    d.hotel_idhotel = ((drzi)Tuple).hotel_idhotel;
                                    d.vlasnik_mbr = ((drzi)Tuple).vlasnik_mbr;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                            

                            break;

                        case "Gost":
                            List<gost> results2 = (from p in db.gost
                                                   where p.jmbg == ((gost)Tuple).jmbg
                                                   select p).ToList();

                            if (results2.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg gosta.";
                            }
                            else
                            {
                                foreach (gost g in results2)
                                {
                                    g.jmbg = ((gost)Tuple).jmbg;
                                    g.ime_gosta = ((gost)Tuple).ime_gosta;
                                    g.prezime_gosta = ((gost)Tuple).prezime_gosta;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                           

                            break;
                        case "Hotel":
                            List<hotel> results3 = (from p in db.hotel
                                                    where p.idhotel == ((hotel)Tuple).idhotel
                                                    select p).ToList();

                            if (results3.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci id hotela.";
                            }
                            else
                            {
                                foreach (hotel h in results3)
                                {
                                    h.idhotel = ((hotel)Tuple).idhotel;
                                    h.naziv = ((hotel)Tuple).naziv;
                                    h.mesto = ((hotel)Tuple).mesto;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                           

                            break;
                        case "Konobar":
                            List<konobar> results4 = (from p in db.konobar
                                                      where p.jmbgradnika == ((konobar)Tuple).jmbgradnika
                                                      select p).ToList();

                            if (results4.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg radnika.";
                            }
                            else
                            {
                                foreach (konobar k in results4)
                                {
                                    k.jmbgradnika = ((konobar)Tuple).jmbgradnika;
                                    k.restoran_idrest = ((konobar)Tuple).restoran_idrest;
                                    k.godine_iskustva = ((konobar)Tuple).godine_iskustva;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }
                            
                            break;
                        case "Menadzer":
                            List<menadzer> results5 = (from p in db.menadzer
                                                       where p.jmbgradnika == ((menadzer)Tuple).jmbgradnika
                                                       select p).ToList();

                            if (results5.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg menadzera.";
                            }
                            else
                            {
                                foreach (menadzer m in results5)
                                {
                                    m.jmbgradnika = ((menadzer)Tuple).jmbgradnika;
                                    m.tip = ((menadzer)Tuple).tip;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                           

                            break;
                        case "Odseda":
                            List<odseda> results6 = (from p in db.odseda
                                                     where p.gost_jmbg == ((odseda)Tuple).gost_jmbg && p.soba_broj == ((odseda)Tuple).soba_broj && p.soba_hotel_idhotel == ((odseda)Tuple).soba_hotel_idhotel
                                                     select p).ToList();

                            if (results6.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg gosta, broj sobe i id hotela.";
                            }
                            else
                            {
                                foreach (odseda o in results6)
                                {
                                    o.gost_jmbg = ((odseda)Tuple).gost_jmbg;
                                    o.soba_broj = ((odseda)Tuple).soba_broj;
                                    o.soba_hotel_idhotel = ((odseda)Tuple).soba_hotel_idhotel;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }

                            break;
                        case "Radnik":
                            List<radnik> results7 = (from p in db.radnik
                                                     where p.jmbgradnika == ((radnik)Tuple).jmbgradnika
                                                     select p).ToList();

                            if (results7.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg radnika.";
                            }
                            else
                            {
                                foreach (radnik r in results7)
                                {
                                    r.jmbgradnika = ((radnik)Tuple).jmbgradnika;
                                    r.ime_radnika = ((radnik)Tuple).ime_radnika;
                                    r.prezime_radnika = ((radnik)Tuple).prezime_radnika;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }
                          
                            break;
                        case "Recepcionar":
                            NewMethod(db);

                            break;
                        case "Restoran":
                            List<restoran> results9 = (from p in db.restoran
                                                       where p.idrest == ((restoran)Tuple).idrest
                                                       select p).ToList();

                            if (results9.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci id restorana.";
                            }
                            else
                            {
                                foreach (restoran r in results9)
                                {
                                    r.idrest = ((restoran)Tuple).idrest;
                                    r.nazivrestorana = ((restoran)Tuple).nazivrestorana;
                                    r.hotel_idhotel = ((restoran)Tuple).hotel_idhotel;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                           

                            break;
                        case "Soba":
                            List<soba> results10 = (from p in db.soba
                                                    where p.broj == ((soba)Tuple).broj && p.hotel_idhotel == ((soba)Tuple).hotel_idhotel
                                                    select p).ToList();

                            if (results10.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci broj sobe.";
                            }
                            else
                            {
                                foreach (soba s in results10)
                                {
                                    s.broj = ((soba)Tuple).broj;
                                    s.sprat = ((soba)Tuple).sprat;
                                    s.hotel_idhotel = ((soba)Tuple).hotel_idhotel;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }

                            }

                            break;
                        case "Spremacica":
                            List<spremacica> results11 = (from p in db.spremacica
                                                          where p.jmbgradnika == ((spremacica)Tuple).jmbgradnika
                                                          select p).ToList();

                            if (results11.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci jmbg spremacice.";
                            }
                            else
                            {
                                foreach (spremacica s in results11)
                                {
                                    s.jmbgradnika = ((spremacica)Tuple).jmbgradnika;
                                    s.ima_opremu = ((spremacica)Tuple).ima_opremu;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }                            

                            break;
                        case "Vlasnik":
                            List<vlasnik> results12 = (from p in db.vlasnik
                                                       where p.mbr == ((vlasnik)Tuple).mbr
                                                       select p).ToList();

                            if (results12.Count == 0)
                            {
                                ValidationError = "Morate uneti postojeci mbr vlasnika.";
                            }
                            else
                            {
                                foreach (vlasnik v in results12)
                                {
                                    v.mbr = ((vlasnik)Tuple).mbr;
                                    v.ime_vlasnika = ((vlasnik)Tuple).ime_vlasnika;
                                    v.prezime_vlasnika = ((vlasnik)Tuple).prezime_vlasnika;
                                }

                                try
                                {
                                    db.SaveChanges();
                                    ValidationError = "";
                                }
                                catch (Exception)
                                {

                                }
                            }
                           
                            break;
                    }
                }
            }
        }

        private void NewMethod(E31032014ProjekatEntities db)
        {
            List<recepcionar> results8 = (from p in db.recepcionar
                                          where p.jmbgradnika == ((recepcionar)Tuple).jmbgradnika
                                          select p).ToList();

            if (results8.Count == 0)
            {
                ValidationError = "Morate uneti postojeci jmbg recepcionara.";
            }
            else
            {
                foreach (recepcionar r in results8)
                {
                    r.jmbgradnika = ((recepcionar)Tuple).jmbgradnika;
                    r.ima_racunar = ((recepcionar)Tuple).ima_racunar;
                }

                try
                {
                    db.SaveChanges();
                    ValidationError = "";
                }
                catch (Exception)
                {

                }
            }
        }

        public void deleteTuple()
        {
            using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
            {
                if (Tuple == null || Tuple.ToString() == "{NewItemPlaceholder}")
                {
                    ValidationError = "Morate izabrati neki red iz tabele.";
                }
                else
                {
                    switch (tableListItem)
                    {
                        case "Drzi":
                            try
                            {
                                db.drzi.Attach((drzi)Tuple);
                                db.drzi.Remove((drzi)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<drzi>(db.drzi.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele.";
                            }

                            break;
                        case "Gost":
                            try
                            {
                                db.gost.Attach((gost)Tuple);
                                db.gost.Remove((gost)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<gost>(db.gost.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg gosta nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Hotel":
                            try
                            {
                                db.hotel.Attach((hotel)Tuple);
                                db.hotel.Remove((hotel)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<hotel>(db.hotel.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se id hotela nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Konobar":
                            try
                            {
                                db.konobar.Attach((konobar)Tuple);
                                db.konobar.Remove((konobar)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<konobar>(db.konobar.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg konobara nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Menadzer":
                            try
                            {
                                db.menadzer.Attach((menadzer)Tuple);
                                db.menadzer.Remove((menadzer)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<menadzer>(db.menadzer.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg menadzera nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Odseda":
                            try
                            {
                                db.odseda.Attach((odseda)Tuple);
                                db.odseda.Remove((odseda)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<odseda>(db.odseda.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele.";
                            }

                            break;
                        case "Radnik":
                            try
                            {
                                db.radnik.Attach((radnik)Tuple);
                                db.radnik.Remove((radnik)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<radnik>(db.radnik.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg radnika nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Recepcionar":
                            try
                            {
                                db.recepcionar.Attach((recepcionar)Tuple);
                                db.recepcionar.Remove((recepcionar)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<recepcionar>(db.recepcionar.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg recepcionara nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Restoran":
                            try
                            {
                                db.restoran.Attach((restoran)Tuple);
                                db.restoran.Remove((restoran)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<restoran>(db.restoran.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se id restorana nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Soba":
                            try
                            {
                                db.soba.Attach((soba)Tuple);
                                db.soba.Remove((soba)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<soba>(db.soba.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se broj sobe nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Spremacica":
                            try
                            {
                                db.spremacica.Attach((spremacica)Tuple);
                                db.spremacica.Remove((spremacica)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<spremacica>(db.spremacica.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se jmbg spremacice nalazi u drugoj tabeli.";
                            }

                            break;
                        case "Vlasnik":
                            try
                            {
                                db.vlasnik.Attach((vlasnik)Tuple);
                                db.vlasnik.Remove((vlasnik)Tuple);
                                db.SaveChanges();

                                Table = new ObservableCollection<vlasnik>(db.vlasnik.ToList());
                                ValidationError = "";
                            }
                            catch (DbUpdateException)
                            {
                                ValidationError = "Ne mozete obrisati red iz ove tabele jer se mbr vlasnika nalazi u drugoj tabeli.";
                            }

                            break;
                    }
                }
            }
        }

        public string ValidationError
        {
            get { return validationError; }
            set { SetProperty(ref validationError, value); }
        }

        public string TableListItem
        {
            get { return tableListItem; }
            set
            {
                if (value != tableListItem)
                {
                    tableListItem = value;
                    OnPropertyChanged("TableListItem");

                    using (E31032014ProjekatEntities db = new E31032014ProjekatEntities())
                    {
                        switch (tableListItem)
                        {
                            case "Drzi":
                                Table = new ObservableCollection<drzi>(db.drzi.ToList());
                                break;
                            case "Gost":
                                Table = new ObservableCollection<gost>(db.gost.ToList());
                                break;
                            case "Hotel":
                                Table = new ObservableCollection<hotel>(db.hotel.ToList());
                                break;
                            case "Konobar":
                                Table = new ObservableCollection<konobar>(db.konobar.ToList());
                                break;
                            case "Menadzer":
                                Table = new ObservableCollection<menadzer>(db.menadzer.ToList());
                                break;
                            case "Odseda":
                                Table = new ObservableCollection<odseda>(db.odseda.ToList());
                                break;
                            case "Radnik":
                                Table = new ObservableCollection<radnik>(db.radnik.ToList());
                                break;
                            case "Recepcionar":
                                Table = new ObservableCollection<recepcionar>(db.recepcionar.ToList());
                                break;
                            case "Restoran":
                                Table = new ObservableCollection<restoran>(db.restoran.ToList());
                                break;
                            case "Soba":
                                Table = new ObservableCollection<soba>(db.soba.ToList());
                                break;
                            case "Spremacica":
                                Table = new ObservableCollection<spremacica>(db.spremacica.ToList());
                                break;
                            case "Vlasnik":
                                Table = new ObservableCollection<vlasnik>(db.vlasnik.ToList());
                                break;
                        }
                    }

                }
            }
        }
    }
}
