<script>
	function borrar(id){
		mostrarAlerta('Alerta','Está seguro de que desea borrar el usuario ID '+id+'?','Borrar',function(){
			window.location = "<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'borrar')); ?>/"+id;
		});
	}
	
	function toggleBan(id){
		mostrarAlerta('Alerta','Está seguro de que desea cambiar el estado de bloqueo del usuario ID '+id+'?','Si',function(){
			window.location = "<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'toggle_ban')); ?>/"+id;
		});
	}
</script>
<div class="table-responsive mb-20">
	<table class="table table-striped table-success">
		<thead>
			<tr>
				<th>Nombre</th>
				<th>Apellido</th>
				<th>Username</th>
				<th>Función</th>
				<th>Bloqueado</th>
				<th class="text-center">Acciones</th>
			</tr>
		</thead>
		<tbody>
			<?php foreach($usuarios as $usuario){ ?>
				<tr>
					<td><?php echo $usuario['Usuario']['nombre']; ?></td>
					<td><?php echo $usuario['Usuario']['apellido']; ?></td>
					<td><?php echo $usuario['Usuario']['username']; ?></td>
					<td><?php echo ($usuario['Usuario']['admin']==1?'Administrador':'Promotora'); ?></td>
					<td><?php echo ($usuario['Usuario']['banned']==1?'Si':'No'); ?></td>
					<td class="text-center">
						<a href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'modificar',$usuario['Usuario']['id'])); ?>" class="btn btn-success btn-xs" data-toggle="tooltip" data-placement="top" data-original-title="Modificar"><i class="fa fa-pencil"></i></a>
						<a href="javascript:void(0);" onClick="borrar(<?php echo $usuario['Usuario']['id']; ?>)" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="top" data-original-title="Borrar"><i class="fa fa-trash"></i></a>
						<a href="javascript:void(0);" onClick="toggleBan(<?php echo $usuario['Usuario']['id']; ?>)" class="btn btn-primary btn-xs"><?php echo ($usuario['Usuario']['banned']==1?'Desbloquear':'Bloquear'); ?></a>
					</td>
				</tr>
			<?php } ?>
		</tbody>
		<!--<tfoot>
			<tr>
				<th>Nombre</th>
				<th>Apellido</th>
				<th>Username</th>
				<th>Función</th>
				<th class="text-center">Acciones</th>
			</tr>
		</tfoot>-->
	</table>
</div>

<style>
	#alert{display: none; width: 100%; height: 100%; position: fixed; left: 0px; top: 0px; background-color: rgba(0, 0, 0, 0.5);}
</style>

<script>
	function mostrarAlerta(titulo, mensaje, ok_text, callback){
		$('#alert .modal-title').text(titulo);
		$('#alert .contenido-modal-body').text(mensaje);
		$('#alert .btn-danger').text(ok_text);
		$('#alert .btn-danger').click(function(){
			OcultarAlerta();
			callback();
		});
		$('#alert').show();
	}

	function OcultarAlerta(){
		$('#alert .btn-danger').unbind('click');
		$('#alert').hide();
	}
</script>

<div id="alert">
	<div class="col-lg-3 col-md-4 col-sm-6" style="margin-top: 10%; margin-left: 35%;">
		<div class="bs-example-modal">
			<div class="modal modal-danger">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true" onClick="OcultarAlerta();">×</button>
							<h4 class="modal-title"></h4>
						</div>
						<div class="modal-body">
							<p class="contenido-modal-body"></p>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal" onClick="OcultarAlerta();">Cancelar</button>
							<button type="button" class="btn btn-danger"></button>
						</div>
					</div>
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->
		</div><!-- /.bs-example-modal -->
	</div>
</div>