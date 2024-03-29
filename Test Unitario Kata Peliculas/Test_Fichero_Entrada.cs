﻿using System;
using System.IO;
using KataPeliculas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Unitario_Kata_Peliculas
{
    [TestClass]
    public class Test_Fichero_Entrada
    {
        private Rhino.Mocks.MockRepository mock;
        private IFicheroEntrada mockficheroEntrada;
        private FicheroEntradaStub fakeFicheroEntrada;

        public Test_Fichero_Entrada()
        {
            mock = new Rhino.Mocks.MockRepository();
            mockficheroEntrada = mock.DynamicMock<IFicheroEntrada>();
            fakeFicheroEntrada = new FicheroEntradaStub();
        }

        [TestMethod]
        public void Existe()
        {
            Rhino.Mocks.Expect.Call(mockficheroEntrada.Existe()).IgnoreArguments().Return(true);

            mock.Replay(mockficheroEntrada);

            Assert.IsTrue(mockficheroEntrada.Existe());

            mock.Verify(mockficheroEntrada);

        }

        [TestMethod]
        public void Existe_Con_Stub_Artesanal()
        {
            FicheroEntradaStub fakeFicheroEntrada = new FicheroEntradaStub();

            IFicheroEntrada ficheroEntrada = fakeFicheroEntrada;
            
            Assert.IsTrue(ficheroEntrada.Existe());

        }

        [TestMethod]
        public void Abrir()
        {
            Rhino.Mocks.Expect.Call(mockficheroEntrada.Abrir()).Return(mock.Stub<StreamReader>());
                       
            mock.Replay(mockficheroEntrada);

            StreamReader contenidoFichero = mockficheroEntrada.Abrir();

            Assert.IsNotNull(contenidoFichero);

            mock.Verify(mockficheroEntrada);
        }

        [TestMethod]
        public void Recorrer_Fichero()
        {
            FakeStreamContenidoFichero fakeStream = new FakeStreamContenidoFichero(false);
            FicheroEntrada ficheroEntrada = new FicheroEntrada();
            bool result = ficheroEntrada.RecorrerFichero(fakeStream.contenidoFichero);
            Assert.IsTrue(result);        
        }

        [TestMethod]
        public void Error_Fichero_Vacio()
        {
            try
            {
                FicheroEntrada ficheroEntrada = new FicheroEntrada();
                bool result = ficheroEntrada.RecorrerFichero(new StreamReader(new MemoryStream()));
                Assert.Fail("Error fichero vacio");
            }
            catch
            {
                //Ok
            }
        }

        [TestMethod]
        public void Cerrar()
        {
            try
            {
                Rhino.Mocks.Expect.Call<IFicheroEntrada>(mockficheroEntrada).Throw(new Exception("No se pude cerrar fichero"));
                mockficheroEntrada.Cerrar(null);
                Assert.Fail("No se puede cerrar fichero");
            }
            catch
            {
                //Ok
            }



        }
    }
}
