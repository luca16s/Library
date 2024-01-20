namespace Library.Tests.Core.Services;

using Library.Tests.Common;
using Library.Tests.Common.Interfaces;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

using Xunit;

public class ServiceTest
{
    [Fact]
    public void DeveVerificarSeMetodoCreateFoiChamadoParaUmItem()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.CreateAsync(new Pessoa(1));

        repositorio.Verify(x => x.CreateAsync(It.IsAny<Pessoa>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoCreateFoiChamadoParaVariosItems()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.CreateAsync(new List<Pessoa>());

        repositorio.Verify(x => x.CreateAsync(It.IsAny<List<Pessoa>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoDeleteFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.Delete(new Pessoa(1));

        repositorio.Verify(x => x.DeleteAsync(It.IsAny<Pessoa>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoGetFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.GetAsync(1);

        repositorio.Verify(x => x.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoGetAllFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.GetAll(5);

        repositorio.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoSearchFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.Search(x => x.Id == 1);

        repositorio.Verify(x => x.Search(It.IsAny<Expression<Func<Pessoa, bool>>>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoUpdateFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.Update(1, new Pessoa(1));

        repositorio.Verify(x => x.UpdateAsync(It.IsAny<long>(), It.IsAny<Pessoa>()), Times.Once);
    }

    [Fact]
    public void DeveVerificarSeMetodoDerivadoDoRepositorioFoiChamado()
    {
        var repositorio = new Mock<IPessoaRepository>();
        var servico = new PessoaService(repositorio.Object);
        _ = servico.GetStringValue(string.Empty);

        repositorio.Verify(x => x.GetStringValue(It.IsAny<string>()), Times.Once);
    }
}
