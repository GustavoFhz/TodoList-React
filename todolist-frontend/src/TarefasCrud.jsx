import React, { useEffect, useState } from "react";



const TarefasCrud = () => {
  const [tarefas, setTarefas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [isCompleted, setIsCompleted] = useState(false);
  const [editId, setEditId] = useState(null);

  // --- Carrega as tarefas ---
  const carregarTarefas = async () => {
    setLoading(true);
    try {
      const response = await fetch("/api/tarefa");
      if (!response.ok) throw new Error("Erro ao carregar tarefas");
      const json = await response.json();
      setTarefas(json.data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarTarefas();
  }, []);

  // --- Criar ou Editar tarefa ---
  const salvarTarefa = async () => {
    const tarefa = { title, description, isCompleted };

    try {
      let url = "/api/tarefa";
      let method = "POST";

      if (editId) {
        tarefa.id = editId;
        method = "PUT";
      }

      const response = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(tarefa),
      });

      if (!response.ok) throw new Error("Erro ao salvar tarefa");
      await carregarTarefas();

      setTitle("");
      setDescription("");
      setIsCompleted(false);
      setEditId(null);
    } catch (err) {
      setError(err.message);
    }
  };

  // --- Excluir tarefa ---
  const removerTarefa = async (id) => {
    if (!window.confirm("Deseja realmente excluir?")) return;

    try {
      const response = await fetch(`/api/tarefa/${id}`, {
        method: "DELETE",
      });
      if (!response.ok) throw new Error("Erro ao remover tarefa");
      await carregarTarefas();
    } catch (err) {
      setError(err.message);
    }
  };

  // --- Preenche os campos para edição ---
  const editarTarefa = (tarefa) => {
    setTitle(tarefa.title);
    setDescription(tarefa.description);
    setIsCompleted(tarefa.isCompleted);
    setEditId(tarefa.id);
  };

  if (loading) return <p>Carregando tarefas...</p>;
  if (error) return <p>Erro: {error}</p>;

  return (
    <div>
      {/* Formulário */}
      <div style={{ marginBottom: "20px" }}>
        <input
          type="text"
          placeholder="Título"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={{ marginRight: "10px" }}
        />
        <input
          type="text"
          placeholder="Descrição"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={{ marginRight: "10px" }}
        />
        <label style={{ marginRight: "10px" }}>
          Concluída:
          <input
            type="checkbox"
            checked={isCompleted}
            onChange={(e) => setIsCompleted(e.target.checked)}
            style={{ marginLeft: "5px" }}
          />
        </label>
        <button onClick={salvarTarefa}>
          {editId ? "Atualizar" : "Adicionar"}
        </button>
        {editId && (
          <button
            onClick={() => {
              setTitle("");
              setDescription("");
              setIsCompleted(false);
              setEditId(null);
            }}
            style={{ marginLeft: "10px" }}
          >
            Cancelar
          </button>
        )}
      </div>

      {/* Lista de tarefas */}
      {tarefas.length === 0 ? (
        <p>Nenhuma tarefa encontrada.</p>
      ) : (
        <ul>
          {tarefas.map((tarefa) => (
            <li key={tarefa.id} style={{ marginBottom: "10px" }}>
              <strong>{tarefa.title}</strong> - {tarefa.description} -{" "}
              {tarefa.isCompleted ? "✔️ Concluída" : "❌ Pendente"}
              <button
                onClick={() => editarTarefa(tarefa)}
                style={{ marginLeft: "10px" }}
              >
                Editar
              </button>
              <button
                onClick={() => removerTarefa(tarefa.id)}
                style={{ marginLeft: "5px" }}
              >
                Excluir
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default TarefasCrud;
