<script>
	function borrar(id){
		mostrarAlerta('Alerta','Esta seguro de que desea borrar el evento ID '+id+'?','Borrar',function(){
			window.location = "<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'borrar')); ?>/"+id;
		});
	}
</script>
<div class="table-responsive mb-20">
	<table class="table table-striped table-success">
		<thead>
			<tr>
				<th>Código único</th><th>Nombre</th><th>Ciudad</th><th>Fecha desde</th><th>Fecha hasta</th><th class="text-center">Acciones</th>
			</tr>
		</thead>
		<tbody>
			<?php foreach($eventos as $evento){ ?>
				<tr>
					<td><?php echo $evento['Evento']['code']; ?></td>
					<td><?php echo $evento['Evento']['nombre']; ?></td>
					<td><?php echo $evento['Evento']['ciudad']; ?></td>
					<td><?php echo date_format(date_create($evento['Evento']['fecha_desde']),'d-m-Y'); ?></td>
					<td><?php echo date_format(date_create($evento['Evento']['fecha_hasta']),'d-m-Y'); ?></td>
					<td class="text-center">
						<a href="<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'modificar',$evento['Evento']['id'])); ?>" class="btn btn-success btn-xs" data-toggle="tooltip" data-placement="top" data-original-title="Modificar"><i class="fa fa-pencil"></i></a>
						<a href="javascript:void(0);" onClick="borrar(<?php echo $evento['Evento']['id']; ?>)" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="top" data-original-title="Borrar"><i class="fa fa-trash"></i></a>
						<a href="<?php echo $this->Html->Url(array('controller'=>'configuraciones','action'=>'gestionarPorEvento',$evento['Evento']['id'])); ?>" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="top" data-original-title="Configuraciones"><i class="fa fa-gear"></i></a>
					</td>
				</tr>
			<?php } ?>
		</tbody>
		<?php if(count($eventos) > 30){ ?>
		<tfoot>
			<tr>
				<th>Código único</th><th>Nombre</th><th>Ciudad</th><th>Fecha desde</th><th>Fecha hasta</th><th class="text-center">Acciones</th>
			</tr>
		</tfoot>
		<?php } ?>
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