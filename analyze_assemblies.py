import json
import os
from pathlib import Path

workspace = r"C:\Users\pierr\Documents\Projets\Pole-projet-3D"
asmdef_files = list(Path(workspace).rglob("*.asmdef"))

# Map GUID to assembly name
guid_to_name = {}
assembly_dependencies = {}

for asmdef_file in asmdef_files:
	with open(asmdef_file, 'r') as f:
		content = json.load(f)
		assembly_name = content.get("name", "Unknown")

		# Get the GUID of this assembly from its meta file
		meta_file = str(asmdef_file) + ".meta"
		if os.path.exists(meta_file):
			with open(meta_file, 'r') as mf:
				meta_content = mf.read()
				# Extract guid from meta file
				for line in meta_content.split('\n'):
					if 'guid:' in line:
						guid = line.split('guid:')[1].strip()
						guid_to_name[guid] = assembly_name
						break

		# Store references by GUID
		references = content.get("references", [])
		assembly_dependencies[assembly_name] = references

# Print all assemblies and their dependencies
print("=== GUID to Assembly Name Mapping ===")
for guid, name in sorted(guid_to_name.items()):
	print(f"{guid}: {name}")

print("\n=== Assembly Dependencies ===")
for assembly, refs in sorted(assembly_dependencies.items()):
	if refs:
		resolved_refs = [guid_to_name.get(ref, ref) for ref in refs]
		print(f"{assembly} -> {resolved_refs}")
	else:
		print(f"{assembly} -> (no dependencies)")

# Check for cycles
def find_cycles(dependencies, guid_map):
	resolved_deps = {}
	for assembly, refs in dependencies.items():
		resolved_refs = [guid_map.get(ref, ref) for ref in refs]
		resolved_deps[assembly] = resolved_refs

	def dfs(node, visited, rec_stack):
		visited.add(node)
		rec_stack.add(node)

		for neighbor in resolved_deps.get(node, []):
			if neighbor not in visited:
				if dfs(neighbor, visited, rec_stack):
					return True
			elif neighbor in rec_stack:
				return True

		rec_stack.remove(node)
		return False

	visited = set()
	cycles = []
	for node in resolved_deps:
		if node not in visited:
			if dfs(node, visited, set()):
				cycles.append(node)

	return cycles

cycles = find_cycles(assembly_dependencies, guid_to_name)
print(f"\n=== Cyclic Dependencies Found ===")
if cycles:
	for cycle in cycles:
		print(f"Cycle involving: {cycle}")
else:
	print("No cycles detected!")
